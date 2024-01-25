using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    Dictionary<string, string> sceneToLevelRecord = new Dictionary<string, string>()
    {
        {"LevelOne", "levelOneRecord"},
        {"LevelTwo", "levelTwoRecord"},
        {"LevelThree", "levelThreeRecord"}
    };
    float timer;

    void Start()
    {
        float time = Time.time;
        timer = time;
    }

    public void SetLevelRecord(string level)
    {
        float end = Time.time;

        int timeDelta = (int)((end - timer) * 1000); // Remove 4th decimal place
        float timeDeltaMilli = (float)timeDelta / 1000; // convert back to milliseconds with comma
        if(PlayerPrefs.GetFloat(sceneToLevelRecord[level]) > timeDeltaMilli || PlayerPrefs.GetFloat(sceneToLevelRecord[level]) == 0)
        {
            PlayerPrefs.SetFloat(sceneToLevelRecord[level], timeDeltaMilli);
        }
        Debug.Log(timeDeltaMilli);
    }
}
