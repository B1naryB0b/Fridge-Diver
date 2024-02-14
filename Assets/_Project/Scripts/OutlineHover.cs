using UnityEngine;

public class OutlineHover : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;

    private Renderer objectRenderer;
    private Material[] originalMaterials;
    private Material[] combinedMaterials;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterials = objectRenderer.materials;
        combinedMaterials = new Material[originalMaterials.Length + 1];
        originalMaterials.CopyTo(combinedMaterials, 0);
        combinedMaterials[combinedMaterials.Length - 1] = outlineMaterial;
    }

    private void OnMouseEnter()
    {
        objectRenderer.materials = combinedMaterials;
    }

    private void OnMouseExit()
    {
        objectRenderer.materials = originalMaterials;
    }
}
