using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButtonCheck : MonoBehaviour {
    int count = 0;

    DataMetricRating dataMetric;
    public VoteHandler difficulty, enjoyment;
	void Start () {
        GetComponent<Button>().interactable = false;
	}

    public void Select()
    {
        count++;
        if(count > 1)
        {
            GetComponent<Button>().interactable = true;
        }
    }

    public void SendVote()
    {
        dataMetric.difficulty = difficulty.vote;
        dataMetric.fun = enjoyment.vote;
       // Debug.Log(difficulty.vote);
        //Debug.Log(enjoyment.vote);
        DataCollector datacoll = DataCollector.getInstance();
        dataMetric.level = (int) datacoll.getLastLevel().level;
        datacoll.createRates(dataMetric);
    }
}
