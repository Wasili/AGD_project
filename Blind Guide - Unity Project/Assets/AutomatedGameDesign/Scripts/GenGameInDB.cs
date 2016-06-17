using UnityEngine;
using System.Collections;

public class GenGameInDB : MonoBehaviour {

    private DataCollector inst;

    public DataMetricLevel.Level level; 

    void Awake()
    {
        inst = DataCollector.getInstance();

        inst.createGame();
        inst.startLevel(level); 
    }
}
