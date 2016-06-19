﻿using UnityEngine;
using System; 
using System.Collections;
using System.Collections.Generic;

public class DataMetricGame : DataMetric
{
    public int session;
    public DateTime starttime;
    public float endTime;
    private List<DataMetricLevel> levels = new List<DataMetricLevel>();
    private int

    public DataMetricGame()
    {
        //Debug.Log("Created game...");
    }

    public void addLevel(DataMetricLevel level)
    {
        Debug.Log("Added level...");
        levels.Add(level);
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
