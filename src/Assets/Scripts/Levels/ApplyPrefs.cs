using UnityEngine;

public class ApplyPrefs : MonoBehaviour
{
    public GameObject player = null;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float localVolume = PlayerPrefs.GetFloat("masterVolume");
            AudioListener.volume = localVolume;
        }
        if(PlayerPrefs.GetInt("health") == 0)
        {
            PlayerPrefs.SetInt("health", 1);
        }

        player.transform.position = new Vector2(PlayerPrefs.GetFloat("playerXCoordinate"), PlayerPrefs.GetFloat("playerYCoordinate"));
    }
}
