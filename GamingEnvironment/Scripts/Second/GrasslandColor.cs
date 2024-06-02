using UnityEngine;

public class GrasslandColorChanger : MonoBehaviour
{
    public Material grasslandMaterial;  // Reference to the grassland material

    void Start()
    {
        // Ensure a material is assigned
        if (grasslandMaterial == null)
        {
            Debug.LogError("Grassland material is not assigned.");
            return;
        }

        // Change the grassland color to red
        grasslandMaterial.color = Color.red;
    }
}
