using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToHiddenScene : MonoBehaviour
{
    public void MoveToSceneHiddenScene(int sceneId)
    {
         SceneManager.LoadScene(sceneId);
    }

    public void SetPlayerYCoordinate(float y)
    {
        PlayerPrefs.SetFloat("playerYCoordinate", y);
    }

    public void SetPlayerXCoordinate(float x)
    {
        PlayerPrefs.SetFloat("playerXCoordinate", x);
    }
}
