using System;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    public void UpdateLevelCompleted(int level)
    {
        PlayerPrefs.SetInt("levelCompleted", level);
    }

    public void UpdateHealth(int health)
    {
        PlayerPrefs.SetInt("health", health);
    }

    public void UpdateLevelCurrentlyLoaded(int level)
    {
        PlayerPrefs.SetInt("levelCurrentlyLoaded", level);
    }

    public void UpdateHasStaff(int n)
    {
        PlayerPrefs.SetInt("shootAbility", n);
    }

    public void ResetTime()
    {
       PlayerPrefs.SetFloat("levelOneRecord", 0);
       PlayerPrefs.SetFloat("levelTwoRecord", 0);
       PlayerPrefs.SetFloat("levelThreeRecord", 0);
    }
}
