using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VoteHandler : MonoBehaviour {
    public Button[] buttons;
    public LevelButtonCheck nextLevel;
    bool selected = false;
    public int vote = -1;

    public void SelectStar(int star)
    {
        vote = star + 1;
        for (int i = star; i > -1; i--)
        {
            buttons[i].GetComponent<Image>().color = new Color(255, 255, 0);
        }

        for (int i = star + 1; i < 5; i++)
        {
            buttons[i].GetComponent<Image>().color = new Color(255, 255, 255);
        }
        if (!selected) nextLevel.Select();
        selected = true;
    }
}
