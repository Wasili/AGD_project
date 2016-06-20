using UnityEngine;
using System.Collections;

public class OnCloseApplication : MonoBehaviour {
    
    void OnApplicationQuit()
    {
        DataCollector datacoll = DataCollector.getInstance();
        save();
    }

    void save()
    {
        DataCollector datacoll = DataCollector.getInstance();
        StartCoroutine(datacoll.Upload(datacoll.getJSSONString()));
    }
}
