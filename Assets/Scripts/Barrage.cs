using UnityEngine;
using UnityEngine.UI;
using Shared;

public class Barrage : MonoBehaviour
{
    [SerializeField] private Vector3[] offsets = default;
    [SerializeField] private GameObject cannonball = default;
    [SerializeField] private float cooldown = 5.0f;
    [SerializeField] private Vector3 ballDir = default;
    [SerializeField] private float ballSpeed = 5.0f;
    [SerializeField] private Slider slider = default;

    private AudioSource source;
    private float timer = 0.0f;

    private Vector3 BallVelocity
    {
        get
        {
            return this.transform.TransformVector(this.ballDir) * this.ballSpeed;
        }
    }

    private void Awake()
    {
        this.timer = this.cooldown;
        this.source = this.GetComponent<AudioSource>();
    }

    public void TryAttack(bool cursed, Vector3 boatSpeed)
    {
        if(this.timer > this.cooldown)
        {
            for(int i = 0; i < this.offsets.Length; i++)
            {
                GameObject go = Instantiate(
                    this.cannonball,
                    this.GetStartPos(i),
                    Quaternion.identity
                );

                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.velocity = this.BallVelocity + boatSpeed;

                if(cursed)
                {
                    Cannonball c = go.GetComponent<Cannonball>();
                    c.AddCurse();
                }

                this.source.Play();
            }

            this.timer = 0.0f;
        }
    }

    private void LateUpdate()
    {
        this.timer += Time.deltaTime;

        if(this.slider != null)
        {
            this.slider.value = 1.0f - Mathf.Min(1.0f, this.timer / this.cooldown);
        }
    }

    private Vector3 GetStartPos(int index)
    {
        return this.transform.position + this.transform.TransformDirection(this.offsets[index]);
    }

    private void OnDrawGizmosSelected()
    {
        for(int i = 0; i < this.offsets.Length; i++)
        {
            Vector3 startPos = this.GetStartPos(i);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(startPos, 0.5f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(startPos, this.BallVelocity);
        }
    }
}
