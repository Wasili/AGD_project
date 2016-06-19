using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; 

public class DataMetricLevel : DataMetric {
    
    public enum Level { Tutorial, Fire1, Fire2, Fire3, Ice1, Ice2, Ice3, Jungle1, Jungle2, Jungle3 }

    private int id;
    public int levelID;
    public DateTime startTime;
    public float endTime;
    public int playerDied;
    public string howPlayerDied;
    private List<DataMetricObstacle> obstacles = new List<DataMetricObstacle>();
    private List<DataMetricAttack> attacks = new List<DataMetricAttack>();
    public int qtyFireAttacks { get; private set; }
    public int qtyIceAttacks { get; private set; }
    public int qtyTelekinesisAttacks { get; private set; }

    public void addAttack(DataMetricAttack attack)
    {
        Debug.Log("added attack...");
        attacks.Add(attack);
    }

    private void getQtyOfAttacks()
    {
        int qtyFire = 0;
        int qtyIce = 0;
        int qtyTelekinesis = 0;
        foreach (DataMetricObstacle obst in obstacles)
        {
            switch (obst.howItDied)
            {
                case "Fire":
                    qtyFire++;
                    break;
                case "Ice":
                    qtyIce++;
                    break;
                case "Telekinesis":
                    qtyTelekinesis++;
                    break;
            }
        }
        this.qtyFireAttacks = qtyFire;
        this.qtyIceAttacks = qtyIce;
        this.qtyTelekinesisAttacks = qtyTelekinesis;
    }

    public void addObstacle(DataMetricObstacle obstacle)
    {
        Debug.Log("Added obstacle...");
        obstacles.Add(obstacle);
        getQtyOfAttacks();
    }

    //public override void saveLocalData()
    //{
    //    queryForSave = "INSERT INTO level (gameID, level, startTime, endTime, playerDied, HowPlyerDied) VALUES("
    //        + "'" + gameID + "'" + ","
    //        + "'" + level.ToString() + "'" + ","
    //        + "'" + startTime + "'" + ","
    //        + "'" + endTime + "'" + ","
    //        + "'" + playerDied + "'" + ","
    //        + "'" + howPlayerDied + "'" + ")";  
    //    DataCollector.getInstance().saveMetric(this);
    //}

    //public override string[] loadLocalData()
    //{
    //    queryforLoad = "";
    //    return null;
    //}
}
