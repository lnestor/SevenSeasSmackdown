using UnityEngine;

public class NullInput : BaseInput
{
    public override Vector2 GetDesiredMovement()
    {
        return Vector2.zero;
    }

    public override bool GetLeftAttack()
    {
        return false;
    }

    public override bool GetRightAttack()
    {
        return false;
    }
}
