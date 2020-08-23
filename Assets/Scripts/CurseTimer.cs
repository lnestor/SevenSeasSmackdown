using UnityEngine;
using UnityEngine.Events;

public class CurseTimer : MonoBehaviour
{
    [SerializeField] private float curseTime = 30.0f;
    [SerializeField] private UnityEvent beforeCurse = default;
    [SerializeField] private UnityEvent onCurse = default;

    private float timer;

    private void Update()
    {
        this.timer += Time.deltaTime;

        if(this.timer > this.curseTime)
        {
            this.beforeCurse.Invoke();
            this.onCurse.Invoke();

            this.timer = 0.0f;
        }
    }

    public void PruneShips()
    {
        foreach(Transform t in GameManager.Instance.GetCursed())
        {
            BoatController bc = t.GetComponent<BoatController>();
            bc.Die();
        }
    }

    public void AddCurse()
    {
        if(GameManager.Instance.UncursedBoatCount != 1)
        {
            Transform t = GameManager.Instance.GetRandomBoat();
            t.GetComponent<BoatController>().AddCurse();
        }
    }
}
