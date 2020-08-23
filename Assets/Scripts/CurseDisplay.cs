using UnityEngine;

public class CurseDisplay : MonoBehaviour
{
    [SerializeField] private PartMaterial[] cursedParts = default;
    [SerializeField] private Material normalMaterial = default;
    [SerializeField] private Material cursedMaterial = default;

    public void ShowCurse()
    {
        this.SetMaterial(this.cursedMaterial);
    }

    public void HideCurse()
    {
        this.SetMaterial(this.normalMaterial);
    }

    private void SetMaterial(Material m)
    {
        for(int i = 0; i < this.cursedParts.Length; i++)
        {
            PartMaterial p = this.cursedParts[i];

            Material[] mats = p.Renderer.materials;
            mats[p.MaterialIndex] = m;
            p.Renderer.materials = mats;
        }
    }

    [System.Serializable]
    public class PartMaterial
    {
        public Renderer Renderer;
        public int MaterialIndex;
    }
}
