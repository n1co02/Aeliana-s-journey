using UnityEngine;
using UnityEngine.UI;

public class SetLockedLevels : MonoBehaviour
{
    public Button level2;
    public Button level3;

    public void setLockedLevels()
    {
        switch(PlayerPrefs.GetInt("levelCompleted"))
        {
            case 0:
                level2.interactable = false;
                level3.interactable = false;
                break;
            case 1:
                level2.interactable = true;
                level3.interactable = false;
                break;
            case 2:
                level2.interactable = true;
                level3.interactable = true;
                break;
        }
    }
}
