using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class DataMetricAttack : DataMetric
{
    public enum Type { Fire, Ice, Telekinesis, Destruction }

    [SerializeField]
    public float attackTime;
    [SerializeField]
    public Type type;

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
