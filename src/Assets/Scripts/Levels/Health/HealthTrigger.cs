using UnityEngine;

public class HealthTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int health = PlayerPrefs.GetInt("health");
        if (health < 3)
        {
            PlayerPrefs.SetInt("health", health + 1);
        }
    }
}
