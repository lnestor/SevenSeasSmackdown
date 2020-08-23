using UnityEngine;

public class PlayerInput : BaseInput
{
    public override Vector2 GetDesiredMovement()
    {
        float forward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        return new Vector2(forward, turn);
    }

    public override bool GetLeftAttack()
    {
        return Input.GetButtonDown("Fire2");
    }

    public override bool GetRightAttack()
    {
        return Input.GetButtonDown("Fire1");
    }
}
