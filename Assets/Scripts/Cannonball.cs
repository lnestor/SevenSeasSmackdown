using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cannonball : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect = default;

    private bool cursed;
    private CurseDisplay curseDisplay;

    private void Awake()
    {
        this.curseDisplay = this.GetComponent<CurseDisplay>();
    }

    private void Update()
    {
        if(this.transform.position.y <= GameManager.Instance.YLevel)
        {
            Destroy(this.gameObject);
        }
    }

    public void AddCurse()
    {
        this.cursed = true;
        this.curseDisplay.ShowCurse();
    }

    private void OnTriggerEnter(Collider other)
    {
        BoatController bc = other.transform.GetComponent<BoatController>();
        if(bc == null)
        {
            Debug.Log(other.gameObject.name, other.gameObject);
        }

        if(this.cursed)
        {
            bc.AddCurse();
        }
        else
        {
            bc.Slow();
        }

        this.effect.transform.parent = other.transform;
        this.effect.Play();
        Destroy(this.gameObject);
    }
}
