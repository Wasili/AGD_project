using System;
using UnityEngine;

[Serializable]
public class DataMetricObstacle : DataMetric
{
    public enum Obstacle { Monkey, FlyingEnemy, Lavaman,
        Panther, Snake, Snowman, FallingRock, FireFinish, Geyser, EndBoss,
        IcePool, Icicle, Lavafall, RollingBoulder, RollingBoulderSurprise, Coconut, LavaBall, SnowBall }

    [SerializeField]
    public Obstacle obstacle;
    [SerializeField]
    public float spawnTime;
    [SerializeField]
    public float defeatedTime;
    [SerializeField]
    public string howItDied;

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
