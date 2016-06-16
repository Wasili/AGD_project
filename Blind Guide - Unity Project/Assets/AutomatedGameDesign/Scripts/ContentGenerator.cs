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
	public int levelDifficulty = 100;
	public int maxDeathDifficultyInfluence = 50;
	public float segmentWidth = 10;
	
	private DataMetricAttack.Type playerType;
	private List<LevelSegment> segments;

	void Start() {

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
		int highest = playerData.fire;
		playerType = DataMetricAttack.Type.Fire;
	}

	void GenerateSegments() {
		//make sure we have a static difficulty curve in our levels
		LevelSegment.Difficulty[] difficultyOrder = { LevelSegment.Difficulty.easy, LevelSegment.Difficulty.medium, LevelSegment.Difficulty.hard, LevelSegment.Difficulty.medium };
		int currentDifficulty = 0;

		//calculate how many segments can fit in the level
		float startPoint = GameObject.FindWithTag("Blindguy").transform.position.x;
		float finishPoint = GameObject.FindWithTag("Finish").transform.position.x;
		int segmentCount = (int)((finishPoint - startPoint) / segmentWidth);

		//create the segments with their desired difficulty
		for (int i = 0; i < segmentCount - 1; i++) {
			segments.Add(new LevelSegment(difficultyOrder[currentDifficulty]));
			currentDifficulty = currentDifficulty < difficultyOrder.Length - 1 ? currentDifficulty + 1 : 0;
		}
		//add a boss segment at the end
		segments.Add(new LevelSegment(LevelSegment.Difficulty.boss));
	}

	void GenerateObstacles() {
		int difficultyCount = 0;

		for (int i = 0; i < segments.Count; i++) {
			//TODO: spawn obstacles for one segment and add segment difficulty score
			switch (segments[i].difficulty) {
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
			}
		}
	}
}
