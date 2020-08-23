[System.Serializable]
public class PID
{
    public float Ideal;

    public float PFactor;
    public float IFactor;
    public float DFactor;

    private float prevV = 0.0f;

    public float GetNext(float v)
    {
        float p = this.PFactor * (v - this.Ideal);
        float d = this.DFactor * (v - this.prevV);

        this.prevV = v;

        return p + d;
    }
}
