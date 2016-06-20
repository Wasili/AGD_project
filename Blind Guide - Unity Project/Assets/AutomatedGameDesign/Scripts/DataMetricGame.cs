using UnityEngine;
using System; 
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class DataMetricGame : DataMetric
{
    [SerializeField]
    public int session;
    [SerializeField]
    public DateTime starttime;
    [SerializeField]
    public float endTime;
    [SerializeField]
    private List<DataMetricLevel> levels = new List<DataMetricLevel>();

    public DataMetricGame()
    {
        System.Random rand = new System.Random(); 
        session = rand.Next(0, int.MaxValue);
        //Debug.Log("Created game...");
    }

    public void addLevel(DataMetricLevel level)
    {
        //Debug.Log("Added level...");
        levels.Add(level);
    }

    public int playerDeadsInLevel(DataMetricLevel.Levels levelKind)
    {
        int qty = 0; 
        foreach(DataMetricLevel level in levels)
        {
            if(level.level == levelKind)
            {
                qty++;
            }
        } 
        return qty; 
    }

    public DataMetricLevel getLastLevel()
    {
        if (levels.Count > 0)
        {
            return levels[levels.Count - 1];
        }
        else
        {
            return null;
        }
    }

    public void saveLastLevel(DataMetricLevel level)
    {
        levels[levels.Count - 1] = level; 
    }

    //public override void saveLocalData()
    //{
    //    queryForSave = "INSERT INTO game(Session, StartTime, EndTime) VALUES("
    //        + "'" + session + "'" + ","
    //        + "'" + starttime + "'" + ","
    //        + "'" + endTime + "'"
    //        + ")";
    //    DataCollector.getInstance().saveMetric(this);
    //}

    //public override string[] loadLocalData()
    //{
    //    queryforLoad = "";
    //    return null;
    //}
}
