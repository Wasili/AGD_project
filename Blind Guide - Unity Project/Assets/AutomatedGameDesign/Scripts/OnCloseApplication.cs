using UnityEngine;
using System.Collections;

public class OnCloseApplication : MonoBehaviour {
    
    void OnApplicationQuit()
    {
        DataCollector datacoll = DataCollector.getInstance();
        datacoll.save();
    }
}
