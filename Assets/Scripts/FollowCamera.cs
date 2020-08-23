using UnityEngine;

[ExecuteInEditMode]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target = default;
    [SerializeField] private Vector3 offset = default;

    private void Update()
    {
        if(this.target != null)
        {
            this.transform.position = this.target.position + this.offset;
            this.transform.LookAt(this.target);
        }
    }
}
