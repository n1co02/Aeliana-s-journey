using UnityEngine;
using UnityEngine.UI;

public class ManageHearts : MonoBehaviour
{
    public Image Heart1 = null;
    public Image Heart2 = null;
    public Image Heart3 = null;

    private void Update()
    {
        AdjustHearts();
    }

    public void AdjustHearts()
    {
        int hearts = PlayerPrefs.GetInt("health");
  
        if(hearts == 0)
        {
            Heart1.enabled = false;
            Heart2.enabled = false;
            Heart3.enabled = false;
        }
        else if (hearts == 1)
        {
            Heart1.enabled = true;
            Heart2.enabled = false;
            Heart3.enabled = false;
        }
        else if (hearts == 2)
        {
            Heart1.enabled = true;
            Heart2.enabled = true;
            Heart3.enabled = false;
        }
        else 
        {
            Heart1.enabled = true;
            Heart2.enabled = true;
            Heart3.enabled = true;
        }
    }
}
