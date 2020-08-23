using UnityEngine;

public class BobAndSway : MonoBehaviour
{
    [Header("Bobbing")]
    [SerializeField, Min(0.0f)]
    private float bobMagnitude = 0.4f;

    [SerializeField, Min(0.0f)]
    private float bobSpeed = 1.0f;

    private float bobT = 0.5f;
    private int bobDir = 1;
    private float bobMax;
    private float bobMin;

    [Header("Swaying")]
    [SerializeField, Min(0.0f), Tooltip("Specified in degrees")]
    private float swayMagnitude = 30.0f;

    [SerializeField, Min(0.0f)]
    private float swaySpeed = 1.0f;

    private float swayT = 0.5f;
    private int swayDir = 1;
    private float swayMax;
    private float swayMin;
    private Vector3 swayAxis;
    private float angle;

    private void Start()
    {
        float y = this.transform.position.y;
        this.bobMax = y + this.bobMagnitude;
        this.bobMin = y - this.bobMagnitude;

        this.bobT = Random.Range(0.0f, 1.0f);

        this.swayMax = 0 + this.swayMagnitude / 2;
        this.swayMin = 0 - this.swayMagnitude / 2;
        this.swayT = Random.Range(0.0f, 1.0f);

        this.swayAxis = new Vector3(Random.value, 0.0f, Random.value).normalized;
    }

    private void Update()
    {
        Vector3 pos = this.transform.position;
        this.BounceBetween(ref pos.y, this.bobMin, this.bobMax, ref this.bobT, ref this.bobDir, this.bobSpeed);
        this.transform.position = pos;

        if(this.swayMagnitude > 0.0f)
        {
            this.BounceBetween(ref this.angle, this.swayMin, this.swayMax, ref this.swayT, ref this.swayDir, this.swaySpeed);
            this.transform.rotation = Quaternion.AngleAxis(this.angle, this.swayAxis);
        }
    }

    private void BounceBetween(ref float data, float min, float max, ref float t, ref int dir, float speed)
    {
        data = Mathf.SmoothStep(min, max, t);

        t += speed * dir * Time.deltaTime;
        if(t >= 1.0f)
        {
            t = 1.0f;
            dir *= -1;
        }
        else if(t <= 0.0f)
        {
            t = 0.0f;
            dir *= -1;
        }
    }
}
