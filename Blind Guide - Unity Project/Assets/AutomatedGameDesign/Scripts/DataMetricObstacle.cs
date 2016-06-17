﻿using System;
using UnityEngine;

public class DataMetricObstacle : DataMetric
{
    public enum Obstacle { Monkey, FlyingEnemy, Lavaman,
        Panther, Snake, Snowman, FallingRock, FireFinish, Geyser, EndBoss,
        IcePool, Icicle, Lavafall, RollingBoulder, RollingBoulderSurprise, Coconut, LavaBall, SnowBall }

    public Obstacle obstacle;
    public float spawnTime;
    public float defeatedTime;
    public string howItDied;
    public int levelID;

    //public override void saveLocalData()
    //{
    //    queryForSave = "INSERT INTO Obstacles(Obstacle, SpawnTime, DefeatedTime, HowItDied, GameID) VALUES(" 
    //        + "'" + obstacle.ToString() + "'" + "," 
    //        + "'" + spawnTime + "'" + "," 
    //        + "'" + defeatedTime + "'" + "," 
    //        + "'" + howItDied + "'" + ","
    //        + "'" + gameID + "'" + ")";

    //    DataCollector.getInstance().saveMetric(this);
    //}

    //public override string[] loadLocalData()
    //{
    //    queryforLoad = "";
    //    return null;
    //}
}
