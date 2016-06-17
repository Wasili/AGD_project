using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContentGenerator : MonoBehaviour {
	struct PlayerData {
		public int deaths;
		public int fire;
		public int ice;
		public int telekinesis;
		public int destruction;
	}
	private PlayerData playerData;

	public GenerationObstacle[] fireObjects, iceObjects, telekinesisObjects, destructionObjects;
	private int fireChance, iceChance, telekinesisChance, destructionChance;

	public int levelDifficulty = 100;
	public int maxDeathDifficultyInfluence = 50;
	public int segmentWidth = 10;
	
	private DataMetricAttack.Type playerType = DataMetricAttack.Type.Fire;
	private List<LevelSegment> segments = new List<LevelSegment>();

	public LevelSegment.Difficulty[] difficultyOrder = { LevelSegment.Difficulty.easy, LevelSegment.Difficulty.medium, LevelSegment.Difficulty.hard, LevelSegment.Difficulty.medium };

	int smallestScore;

	void Start() {
		smallestScore = fireObjects[0].difficulty;
		for (int i = 0; i < fireObjects.Length; i++) {
			if (fireObjects[i].difficulty < smallestScore) smallestScore = fireObjects[i].difficulty;
		}
		for (int i = 0; i < iceObjects.Length; i++) {
			if (iceObjects[i].difficulty < smallestScore) smallestScore = iceObjects[i].difficulty;
		}
		for (int i = 0; i < telekinesisObjects.Length; i++) {
			if (telekinesisObjects[i].difficulty < smallestScore) smallestScore = telekinesisObjects[i].difficulty;
		}
		for (int i = 0; i < destructionObjects.Length; i++) {
			if (destructionObjects[i].difficulty < smallestScore) smallestScore = destructionObjects[i].difficulty;
		}
		ReadPlayerData();
		DeterminePlayerType();
		GenerateSegments();
		GenerateObstacles();
	}

	void ReadPlayerData() {
		//TODO: read player data from database
		playerData.deaths = 5;
		playerData.fire = 9;
		playerData.ice = 6;
		playerData.telekinesis = 5;
		playerData.destruction = 3;
	}

	void DeterminePlayerType() {
		//TODO: use player data to determine type
		fireChance = 30;
		iceChance = 20;
		telekinesisChance = 20;
		destructionChance = 30;
		int highest = playerData.fire;
		playerType = DataMetricAttack.Type.Fire;
	}

	void CreateSegment(LevelSegment.Difficulty diff, float x) {
		GameObject go = new GameObject("Segment");
		go.transform.position = new Vector3(x, 0, 0);
		segments.Add(go.AddComponent<LevelSegment>().SetDifficulty(diff));
	}

	void GenerateSegments() {
		//make sure we have a static difficulty curve in our levels
		int currentDifficulty = 0;

		//calculate how many segments can fit in the level
		float startPoint = GameObject.FindWithTag("Blindguy").transform.position.x + 15;
		float finishPoint = GameObject.FindWithTag("Finish").transform.position.x;
		int segmentCount = (int)((finishPoint - startPoint) / segmentWidth);
		int scoreCount = 0;

		//create the segments with their desired difficulty
		for (int i = 1; i < segmentCount - 1; i++) {
			float x = startPoint + i * segmentWidth;
			CreateSegment(difficultyOrder[currentDifficulty], x);
			scoreCount += (int)difficultyOrder[currentDifficulty];
			currentDifficulty = currentDifficulty < difficultyOrder.Length - 1 ? currentDifficulty + 1 : 0;
		}
		//add a boss segment at the end
		CreateSegment(LevelSegment.Difficulty.boss, startPoint + (segmentCount - 1) * segmentWidth);
		SetSegmentScores(scoreCount);
	}

	void SetSegmentScores(int count) {
		int scoreBasis = levelDifficulty / count;
		for (int i = 0; i < segments.Count; i++) {
			segments[i].SetScore(scoreBasis * (int)segments[i].difficulty);
		}
	}

	void GenerateObstacles() {
		for (int i = 0; i < segments.Count; i++) {
			//TODO: spawn obstacles for one segment and add segment difficulty score
			/*switch (segments[i].difficulty) {
				case LevelSegment.Difficulty.easy:
					//spawn obstacles
					//increment difficulty count
					break;

				case LevelSegment.Difficulty.medium:
					break;

				case LevelSegment.Difficulty.hard:
					break;

				case LevelSegment.Difficulty.boss:
					break;
			}*/
			
			int difficultyCount = 0;

			float[] segmentPositions = { -(segmentWidth / 3) + 1,
				0,
				(segmentWidth / 3)  - 1};
			int position = 0;

			while (difficultyCount + smallestScore <= segments[i].difficultyScore && position <= 3) {
				//pick an obstacle type
				int randomType = Random.Range(0, 100);
				GenerationObstacle[] obstacles;
				if (randomType < fireChance) {
					obstacles = fireObjects;
				}
				else if (randomType < fireChance + iceChance) {
					obstacles = iceObjects;
				}
				else if (randomType < fireChance + iceChance + telekinesisChance) {
					obstacles = telekinesisObjects;
				}
				else {
					obstacles = destructionObjects;
				}

				int randomObstacle = Random.Range(0, obstacles.Length);
				if (obstacles[randomObstacle].difficulty + difficultyCount <= segments[i].difficultyScore) {
					Debug.Log(randomObstacle);
					GameObject obs = ((GameObject)Instantiate(obstacles[randomObstacle].gameObject,
						new Vector3(0, obstacles[randomObstacle].transform.position.y, 0),
						obstacles[randomObstacle].transform.rotation));
					obs.transform.parent = segments[i].transform;
					obs.transform.localPosition = new Vector3(segmentPositions[position++], obs.transform.localPosition.y, 0);					
					difficultyCount += obstacles[randomObstacle].difficulty;
				}
			}
		}
	}
}
