using System.Collections;
using UnityEngine;

public class Sink : MonoBehaviour
{
    [SerializeField] private float sinkSpeed = 5.0f;

    public void Activate()
    {
        StartCoroutine(this.DoSink());
    }

    private IEnumerator DoSink()
    {
        Vector3 pos = this.transform.position;

        while(pos.y > -10.0f)
        {
            pos.y -= Time.deltaTime * this.sinkSpeed;
            this.transform.position = pos;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}
