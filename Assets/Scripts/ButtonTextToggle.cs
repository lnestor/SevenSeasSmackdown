using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonTextToggle : MonoBehaviour
{
    [SerializeField] private string firstOption = default;
    [SerializeField] private string secondOption = default;

    private bool onFirst = true;
    private TextMeshProUGUI text;

    private void Awake()
    {
        this.text = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        this.text.text = this.firstOption;
    }

    public void Toggle()
    {
        if(this.onFirst)
        {
            this.text.text = this.secondOption;
        }
        else
        {
            this.text.text = firstOption;
        }

        this.onFirst = !this.onFirst;
    }
}
