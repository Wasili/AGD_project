using System;

public class DataMetricObstacle : DataMetric
{
    public enum Obstacle { Monkey, FlyingEnemy, Lavaman, Panther, Snake, Snowman, FallingRock, FireFinish, Geyser, IcePool, Icicly, Lavafall, RollingBoulder, RollingBoulderSurprise }

    public Obstacle obstacle;
    public string spawnTime;
    public string defeatedTime;
    public string howItDied;
    public int gameID;

    public override void saveLocalData()
    {
        queryForSave = "INSERT INTO Obstacles(Obstacle, SpawnTime, DefeatedTime, HowItDied, GameID) VALUES(" 
            + "'" + obstacle.ToString() + "'" + "," 
            + "'" + spawnTime + "'" + "," 
            + "'" + defeatedTime + "'" + "," 
            + "'" + howItDied + "'" + ","
            + "'" + gameID + "'" + ")";

        DataCollector.getInstance().saveMetric(this);
    }

    public override string[] loadLocalData()
    {
        queryforLoad = "";
        return null;
    }
}
