using UnityEngine;
using System.Collections;

public class GenGameInDB : MonoBehaviour {

    private DataCollector inst;

    public DataMetricLevel.Levels level; 

    void Awake()
    {
        inst = DataCollector.getInstance();

        inst.createGame();
        inst.startLevel(level);
    }

    void OnApplicationQuit()
    {
        DataCollector datacoll = DataCollector.getInstance();
        datacoll.save();
    }
}
