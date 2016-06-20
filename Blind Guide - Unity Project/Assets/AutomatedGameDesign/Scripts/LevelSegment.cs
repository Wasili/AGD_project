using UnityEngine;
using System.Collections;

public class LevelSegment : MonoBehaviour {
	public enum Difficulty { boss, easy, medium, hard }
	public Difficulty difficulty { get; private set; }
	public int difficultyScore;// { get; private set; }

	public LevelSegment SetDifficulty(Difficulty difficulty) {
		this.difficulty = difficulty;
		return this;
	}

	public void SetScore(int score) {
		difficultyScore = score;
	}
}
