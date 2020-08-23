using UnityEngine;

public class AIInput : BaseInput
{
    [SerializeField] private PID pid = default;
    [SerializeField] private float moveAngle = 100.0f;
    [SerializeField] private float arriveDistance = 5.0f;
    [SerializeField] private Vector3 leftBoxOffset = default;
    [SerializeField] private Vector3 leftBoxSize = default;
    [SerializeField] private Vector3 rightBoxOffset = default;
    [SerializeField] private Vector3 rightBoxSize = default;
    [SerializeField] private LayerMask attackLayer = default;
    [SerializeField] private float stuckThreshold = 2.0f;

    private Vector3 target;
    private bool atTarget = true;
    private float stuckTimer = 0.0f;
    private Rigidbody rb;

    private void Awake()
    {
        this.rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(this.rb.velocity.magnitude < 0.5f)
        {
            this.stuckTimer += Time.deltaTime;

            if(this.stuckTimer > this.stuckThreshold)
            {
                this.ChooseTarget();
            }
        }
        else
        {
            this.stuckTimer = 0.0f;
        }
    }

    public override Vector2 GetDesiredMovement()
    {
        if(this.atTarget)
        {
            this.ChooseTarget();
        }

        float forward = this.GetForward();
        float turn = this.GetTurn();

        this.atTarget = Vector3.Distance(this.transform.position, this.target) < this.arriveDistance;

        return new Vector2(forward, turn);
    }

    public override bool GetLeftAttack()
    {
        Collider[] hits = Physics.OverlapBox(
            this.transform.position + this.transform.TransformDirection(this.leftBoxOffset),
            this.leftBoxSize,
            this.transform.localRotation,
            this.attackLayer
        );

        return hits.Length > 0;
    }

    public override bool GetRightAttack()
    {
        Collider[] hits = Physics.OverlapBox(
            this.transform.position + this.transform.TransformDirection(this.rightBoxOffset),
            this.rightBoxSize,
            this.transform.localRotation,
            this.attackLayer
        );

        return hits.Length > 0;
    }

    private float GetForward()
    {
        if(this.atTarget)
        {
            return -1;
        }

        Vector3 dir = (this.target - this.transform.position).normalized;
        float angle = Vector3.Angle(this.transform.forward, dir);

        if(angle <= this.moveAngle)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private float GetTurn()
    {
        Vector3 dir = (this.target - this.transform.position).normalized;
        float dot = Vector3.Dot(this.transform.right, dir);

        return this.pid.GetNext(dot);
    }

    private void ChooseTarget()
    {
        this.target = GameManager.Instance.RandomPoint;
        this.atTarget = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.target, 1.0f);

        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.DrawWireCube(
            this.leftBoxOffset,
            this.leftBoxSize * 2
        );

        Gizmos.DrawWireCube(
            this.rightBoxOffset,
            this.rightBoxSize * 2
        );
    }
}
