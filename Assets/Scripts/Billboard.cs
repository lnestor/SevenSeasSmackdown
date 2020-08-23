using UnityEngine;

[ExecuteInEditMode]
public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform target = default;

    private void Update()
    {
        this.transform.LookAt(this.target.position, Vector3.up);
    }
}
