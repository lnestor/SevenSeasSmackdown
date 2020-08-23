using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    [SerializeField] private AnimationCurve alphaCurve = default;
    [SerializeField] private float endTime = 1.0f;

    private CanvasGroup group;

    private void Awake()
    {
        this.group = this.GetComponent<CanvasGroup>();
    }

    public void StartFlash()
    {
        StartCoroutine(this.FlashInternal());
    }

    private IEnumerator FlashInternal()
    {
        float t = 0.0f;

        while(t <= this.endTime)
        {
            this.group.alpha = this.alphaCurve.Evaluate(t);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
