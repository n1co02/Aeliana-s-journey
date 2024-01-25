using UnityEngine;

public class HiddenSceneTrigger : MonoBehaviour
{
    public GameObject hiddenScenePanel = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hiddenScenePanel.SetActive(true);
    }
}
