using System.Collections.Generic;
using UnityEngine;

public class CureSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cure = default;

    private List<Cure> cures = new List<Cure>();

    public void RemoveAll()
    {
        foreach(Cure c in this.cures)
        {
            c.Sink();
        }

        this.cures.Clear();
    }

    public void Spawn()
    {
        // int count = (GameManager.Instance.BoatCount / 2);
        int count = GameManager.Instance.BoatCount - 1;

        for(int i = 0; i < count; i++)
        {
            Vector3 point = GameManager.Instance.RandomPoint;
            point.y = -0.5f;

            GameObject go = Instantiate(
                this.cure,
                point,
                Quaternion.identity
            );

            Cure c = go.GetComponent<Cure>();
            c.Spawner = this;
            this.cures.Add(c);
        }
    }

    public void MarkCollected(Cure cure)
    {
        this.cures.Remove(cure);
    }
}
