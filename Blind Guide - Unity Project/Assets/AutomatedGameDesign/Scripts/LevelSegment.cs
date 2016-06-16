using UnityEngine;
using System.Collections;

public class LevelSegment {
	public enum Difficulty { easy, medium, hard, boss }
	public Difficulty difficulty { get; private set; }

	public LevelSegment(Difficulty difficulty) {
		this.difficulty = difficulty;
	}
}
