using UnityEngine;
using System.Collections;

public class DataMetric : MonoBehaviour
{
    [@System.Serializable]
    public struct metricValue
    {
        public string name;
        public float value;
    };

    public string metricName;
    public string metricDetails;
    public metricValue[] metricValues;
    public string jsonData { get; private set; }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
