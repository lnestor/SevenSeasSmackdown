using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Fade : MonoBehaviour
{
    [SerializeField] private float fadeTime = 1.0f;
    [SerializeField] private float waitTime = 1.0f;
    [SerializeField] private UnityEvent onFinish = default;

    private CanvasGroup group = default;

    private void Awake()
    {
        this.group = this.GetComponent<CanvasGroup>();
    }

    public void FadeOut()
    {
        StartCoroutine(this.FadeInternal(0.0f, 1.0f));
    }

    public void FadeIn()
    {
        StartCoroutine(this.FadeInternal(1.0f, 0.0f));
    }

    private IEnumerator FadeInternal(float start, float end)
    {
        float alpha = start;

        while(alpha < end)
        {
            this.group.alpha = alpha;
            alpha += Time.deltaTime / this.fadeTime;
            yield return null;
        }

        yield return new WaitForSeconds(this.waitTime);

        this.onFinish.Invoke();
    }
}
