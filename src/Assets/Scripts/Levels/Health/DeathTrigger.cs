using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    float time;

    private void Start()
    {
        time = Time.time;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int currentHealth = PlayerPrefs.GetInt("health");
        if (currentHealth > 0)
        {
            if(Time.time - time > 3)
            {
                PlayerPrefs.SetInt("health", currentHealth - 1);
                time = Time.time;
            }
        }
    }
}
