using UnityEngine;
using System.Collections;

public class DataMetricAttack : DataMetric
{
    public enum Type { Fire, Ice, Telekinesis, Destruction }

    public Time attackTime;
    public Type type;
    public int levelID;

    //public override void saveLocalData()
    //{
    //    queryForSave = "INSERT INTO Attack(AttackTime, Type, GameID) VALUES("
    //        + "'" + attackTime + "'" + ","
    //        + "'" + type.ToString() + "'" + ","
    //        + "'" + gameID + "'" + ")";
    //    DataCollector.getInstance().saveMetric(this);
    //}

    //public override string[] loadLocalData()
    //{
    //    queryforLoad = "";
    //    return null;
    //}
}
