using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float yLevel = 0.0f;
    [SerializeField] private float arenaRadius = 100.0f;

    [SerializeField] private UnityEvent onWin = default;
    [SerializeField] private UnityEvent onLose = default;

    private List<Transform> boats = new List<Transform>();
    private List<Transform> toRemove = new List<Transform>();
    private Transform player;

    public float Radius { get { return arenaRadius; } }
    public float YLevel { get { return yLevel; } }
    public int BoatCount { get { return this.boats.Count; } }
    public int UncursedBoatCount { get { return this.boats.Count - this.toRemove.Count; } }

    public Vector3 RandomPoint
    {
        get
        {
            Vector3 dir = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)).normalized;
            float magnitude = Random.Range(0, arenaRadius);
            return dir * magnitude;
        }
    }

    private void Start()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Boat");
        foreach(GameObject go in gos)
        {
            this.boats.Add(go.transform);

            PlayerInput p = go.GetComponent<PlayerInput>();
            if(p != null)
            {
                this.player = go.transform;
            }
        }
    }

    private void LateUpdate()
    {
        if(this.toRemove.Count > 0)
        {
            foreach(Transform t in this.toRemove)
            {
                this.boats.Remove(t);

                if(t == this.player)
                {
                    StartCoroutine(this.EndGame(this.onLose));
                }
            }

            if(this.boats.Count == 1)
            {
                StartCoroutine(this.EndGame(this.onWin));
            }

            this.toRemove.Clear();
        }
    }

    public Transform[] GetCursed()
    {
        List<Transform> cursed = new List<Transform>();

        foreach(Transform t in this.boats)
        {
            BoatController bc = t.GetComponent<BoatController>();
            if(bc.IsCursed)
            {
                cursed.Add(t);
            }
        }

        return cursed.ToArray();
    }

    public Transform GetRandomBoat()
    {
        var random = new System.Random();
        int index = random.Next(this.boats.Count);
        return this.boats[index];
    }

    public void MarkDead(Transform t)
    {
        this.toRemove.Add(t);
    }

    private IEnumerator EndGame(UnityEvent e)
    {
        yield return new WaitForSeconds(2.0f);
        e.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, this.arenaRadius);
    }
}
