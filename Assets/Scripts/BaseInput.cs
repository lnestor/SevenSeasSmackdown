using UnityEngine;

public abstract class BaseInput : MonoBehaviour
{
    public abstract Vector2 GetDesiredMovement();
    public abstract bool GetLeftAttack();
    public abstract bool GetRightAttack();
}
