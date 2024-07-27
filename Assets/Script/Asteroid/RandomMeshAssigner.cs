using UnityEngine;

public class RandomMeshAssigner : MonoBehaviour
{
    public Mesh[] meshOptions; // Array of Mesh options to choose from

    void Start()
    {
        AssignRandomMesh();
    }

    void AssignRandomMesh()
    {
        if (meshOptions.Length == 0)
        {
            Debug.LogWarning("No mesh options assigned.");
            return;
        }

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("No MeshFilter component found on this GameObject.");
            return;
        }

        int randomIndex = Random.Range(0, meshOptions.Length);
        meshFilter.mesh = meshOptions[randomIndex];
    }
}
