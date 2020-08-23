using UnityEngine;

[RequireComponent(typeof(Camera)), ExecuteInEditMode]
public class SetInteractiveShaderEffects : MonoBehaviour
{
    [SerializeField] private RenderTexture rt = default;
    [SerializeField] private Transform target = default;

    private void Awake()
    {
        Camera cam = this.GetComponent<Camera>();

        Shader.SetGlobalTexture("_RippleRT", rt);
        Shader.SetGlobalFloat("_CamSize", cam.orthographicSize);
    }

    private void LateUpdate()
    {
        if(this.target != null)
        {
            this.transform.position = new Vector3(
                this.target.position.x,
                this.transform.position.y,
                this.target.position.z
            );
            this.transform.LookAt(this.target);

            Shader.SetGlobalVector("_CamPos", this.transform.position);
        }
    }
}
