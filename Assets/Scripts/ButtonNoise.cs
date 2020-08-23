using UnityEngine;
using Shared;

public class ButtonNoise : MonoBehaviour
{
    public void Click()
    {
        Audio.Instance.Play("Button Click");
    }
}
