using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContentGenerator : MonoBehaviour {
	public GameObject[] fireObjects, iceObjects, telekinesisObjects, destructionObjects;
	public int levelDifficulty = 100;
	public int maxDeathCountInfluence = 50;

	private int playerDeathCount;
	private DataMetricAttack.Type playerType;
	private List<LevelSegment> segments;
	
	void Awake() {

	}
}
