using UnityEngine;

public class GainShootAbililty : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetInt("shootAbility", 1);
    }
}
