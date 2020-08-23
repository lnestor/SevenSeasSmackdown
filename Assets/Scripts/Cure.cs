using UnityEngine;

public class Cure : MonoBehaviour
{
    [HideInInspector] public CureSpawner Spawner;

    private Sink sink;

    private void Awake()
    {
        this.sink = this.GetComponent<Sink>();
    }

    public void Sink()
    {
        this.sink.Activate();
    }

    private void OnTriggerEnter(Collider other)
    {
        BoatController bc = other.transform.GetComponent<BoatController>();
        bc.RemoveCurse();
        this.Spawner.MarkCollected(this);
        Destroy(this.gameObject);
    }
}
