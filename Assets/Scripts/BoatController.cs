using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private BaseInput input = default;
    [SerializeField] private float force = 5.0f;
    [SerializeField] private float slowMultiplier = 0.05f;
    [SerializeField] private float maxSpeed = 50.0f;
    [SerializeField] private float rotSpeed = 0.5f;
    [SerializeField] private float slowDrag = 2.0f;
    [SerializeField] private float slowTime = 5.0f;

    [Header("Attack")]
    [SerializeField] private Barrage leftBarrage = default;
    [SerializeField] private Barrage rightBarrage = default;

    [Header("Curse")]
    [SerializeField] private bool cursed = false;

    public bool IsCursed { get { return cursed; } }

    private float slowTimer = 0.0f;
    private Rigidbody rb;
    private bool dead = false;
    private CurseDisplay curseDisplay;
    private Sink sink;
    private AudioSource source;
    private float initialDrag;
    private Vector2 desiredInput;

    private void Awake()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.curseDisplay = this.GetComponent<CurseDisplay>();
        this.sink = this.GetComponent<Sink>();
        this.source = this.GetComponent<AudioSource>();
        this.initialDrag = this.rb.drag;
    }

    private void Update()
    {
        if(this.dead)
        {
            return;
        }

        desiredInput = this.input.GetDesiredMovement();
        if(desiredInput.x < 0.0f)
        {
            desiredInput.x *= this.slowMultiplier;
        }

        this.HandleAttack();
        this.HandleSlow();
    }

    private void FixedUpdate()
    {
        this.rb.AddForce(desiredInput.x * this.transform.forward * this.force, ForceMode.Acceleration);
        Vector3 localVelocity = this.transform.InverseTransformDirection(this.rb.velocity);

        if(Mathf.Abs(desiredInput.y) > 0.001f)
        {
            Quaternion rot = this.transform.localRotation;
            rot *= Quaternion.Euler(0, desiredInput.y * this.rotSpeed * (localVelocity.z + 4.0f), 0);
            this.transform.localRotation = rot;
        }

        if(localVelocity.z < 0.0f)
        {
            localVelocity.z = 0.0f;
        }
        else if(localVelocity.z > this.maxSpeed)
        {
            localVelocity.z = this.maxSpeed;
        }

        this.rb.velocity = this.transform.TransformDirection(localVelocity);
    }

    public void AddCurse()
    {
        this.cursed = true;
        this.curseDisplay.ShowCurse();
    }

    public void RemoveCurse()
    {
        this.cursed = false;
        this.curseDisplay.HideCurse();
    }

    public void Die()
    {
        this.dead = true;
        GameManager.Instance.MarkDead(this.transform);
        this.sink.Activate();
        this.source.Play();
    }

    public void Slow()
    {
        this.rb.drag = this.slowDrag;
        this.slowTimer = 0.0f;
    }

    private void HandleAttack()
    {
        if(this.input.GetLeftAttack())
        {
            this.leftBarrage.TryAttack(this.cursed, this.rb.velocity);
        }

        if(this.input.GetRightAttack())
        {
            this.rightBarrage.TryAttack(this.cursed, this.rb.velocity);
        }
    }

    private void HandleSlow()
    {
        this.slowTimer += Time.deltaTime;

        if(this.rb.drag == this.slowDrag && this.slowTimer > this.slowTime)
        {
            this.rb.drag = this.initialDrag;
        }
    }
}
