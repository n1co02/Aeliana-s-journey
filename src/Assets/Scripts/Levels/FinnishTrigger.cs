using UnityEngine;

public class FinnishTrigger : MonoBehaviour
{
    public GameObject finnishPannel = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        finnishPannel.SetActive(true);
    }
}
