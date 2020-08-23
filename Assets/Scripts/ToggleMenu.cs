using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public void Toggle()
    {
        if(this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
