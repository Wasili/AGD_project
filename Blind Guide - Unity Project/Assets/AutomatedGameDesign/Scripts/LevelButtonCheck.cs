using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButtonCheck : MonoBehaviour {
    int count = 0;
    // Use this for initialization
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
        Debug.Log(difficulty.vote);
        Debug.Log(enjoyment.vote);
    }
}
