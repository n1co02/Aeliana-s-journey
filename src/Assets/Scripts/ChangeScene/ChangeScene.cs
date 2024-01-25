using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MoveToScene(int sceneId)
    {
        int level = PlayerPrefs.GetInt("levelCompleted");
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
