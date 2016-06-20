using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContentGenerator : MonoBehaviour {
    
	struct PowerUsage {
		public DataMetricAttack.Type type;
		public int usage;
		public int spawnChance;
	}
	private PowerUsage[] powerData = new PowerUsage[4];

	public GenerationObstacle[] fireObjects, iceObjects, telekinesisObjects, destructionObjects;

	public int levelDifficulty = 100;
	public int segmentWidth = 10;

	private int playerDeaths;
	private int maxDeathDifficultyInfluence = 50;
	private List<LevelSegment> segments = new List<LevelSegment>();

	public LevelSegment.Difficulty[] difficultyOrder = { LevelSegment.Difficulty.easy, LevelSegment.Difficulty.medium, LevelSegment.Difficulty.hard, LevelSegment.Difficulty.medium };

	int smallestScore;

	void Start() {
		//find the smallest difficulty value in our spawnable objects for use during obstacle generation
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
		CalculateTypePercentages();
		GenerateSegments();
		GenerateObstacles();
	}

	void ReadPlayerData() {
		powerData[0].type = DataMetricAttack.Type.Fire;
		powerData[1].type = DataMetricAttack.Type.Ice;
		powerData[2].type = DataMetricAttack.Type.Telekinesis;
		powerData[3].type = DataMetricAttack.Type.Destruction;

        DataCollector datacoll = DataCollector.getInstance();
        DataMetricLevel prevLevel = datacoll.getLastLevel(); 
		powerData[0].usage = prevLevel.qtyFireAttacks;
		powerData[1].usage = prevLevel.qtyIceAttacks;
        powerData[2].usage = prevLevel.qtyFireAttacks;
        powerData[3].usage = prevLevel.qtyFireAttacks;

        playerDeaths = 0; // datacoll.playerDeadsInLevel(DataMetricLevel.Levels.);
	}

	void CalculateTypePercentages() {
		int highestUsage = 0;
		int highestType = 0;
		//loop through all powers to see which one is used the most
		for (int i = 0; i < powerData.Length; i++) {
			//the default spawn power for all obstacles is 2
			powerData[i].spawnChance = 2;
			if (powerData[i].usage > highestUsage) {
				highestType = i;
				highestUsage = powerData[i].usage;
			}
		}
		//set the spawn chance of the most used to power to 1
		powerData[highestType].spawnChance = 1;
	}

	void GenerateSegments() {
		float difficulty = levelDifficulty, influence = maxDeathDifficultyInfluence, deaths = playerDeaths;

		levelDifficulty = (int)(difficulty - (influence * (Mathf.Min(deaths, 10) / 10)));
		

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

	void CreateSegment(LevelSegment.Difficulty diff, float x) {
		GameObject go = new GameObject("Segment");
		go.transform.position = new Vector3(x, 0, 0);
		segments.Add(go.AddComponent<LevelSegment>().SetDifficulty(diff));
	}

	void SetSegmentScores(int count) {
		//calculate the 'easy' score 
		int scoreBasis = levelDifficulty / count;
		for (int i = 0; i < segments.Count; i++) {
			//multiply the easy score by the desired difficulty (medium=2*easy, hard=3*easy)
			segments[i].SetScore(scoreBasis * (int)segments[i].difficulty);
		}
	}

	void GenerateObstacles() {
		for (int i = 0; i < segments.Count; i++) {
			//keep track of the total difficulty of obstacles in this segment
			int difficultyCount = 0;

			//store positions for obstacles (only 3 obstacles allowed per segment)
			float[] segmentPositions = { -(segmentWidth / 2) + 2,
				0,
				(segmentWidth / 2)  - 2};
			int position = 0;

			//prevent infinite loops
			int fallback = 0;
			//spawn obstacles until the segment difficulty score has been reached (never spawn more than 3 obstacles in one segment)
			while (difficultyCount + smallestScore <= segments[i].difficultyScore && position < 3 && fallback++ < 100) {
				//pick an obstacle type
				int randomType = Random.Range(0, powerData[0].spawnChance + powerData[1].spawnChance + powerData[2].spawnChance + powerData[3].spawnChance);
				GenerationObstacle[] obstacles;
				if (randomType < powerData[0].spawnChance) {
					obstacles = fireObjects;
				}
				else if (randomType < powerData[0].spawnChance + powerData[1].spawnChance) {
					obstacles = iceObjects;
				}
				else if (randomType < powerData[0].spawnChance + powerData[1].spawnChance + powerData[2].spawnChance) {
					obstacles = telekinesisObjects;
				}
				else {
					obstacles = destructionObjects;
				}

				//pick a radom obstacle
				int randomObstacle = Random.Range(0, obstacles.Length);
				//make sure the obstacle is not too difficult for the segment
				if (obstacles[randomObstacle].difficulty + difficultyCount <= segments[i].difficultyScore) {
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
