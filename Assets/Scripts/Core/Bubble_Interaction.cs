using UnityEngine;

public class Bubble_Interaction : MonoBehaviour
{
    
   public float forceAmount = 0.1f;  // Strength of the force applied
    public float radius = 0.5f;       // Radius of effect around the mouse point

    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] modifiedVertices;

    void Start()
    {
        // Get the SkinnedMeshRenderer
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("No SkinnedMeshRenderer found on " + gameObject.name);
            return;
        }

        // Clone the mesh so we don't modify the original
        mesh = Instantiate(skinnedMeshRenderer.sharedMesh);
        skinnedMeshRenderer.sharedMesh = mesh;

        // Store original vertex positions
        originalVertices = mesh.vertices;
        modifiedVertices = new Vector3[originalVertices.Length];
        originalVertices.CopyTo(modifiedVertices, 0);
    }

    void Update()
    {
        // Create a new baked mesh that we can modify
        Mesh bakedMesh = new Mesh();
        skinnedMeshRenderer.BakeMesh(bakedMesh); // Get an editable version of the mesh

        Vector3[] vertices = bakedMesh.vertices;

        // Raycast to detect the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            ApplyForceAtPoint(hit.point, vertices);
        }

        // Assign the modified vertices back to the mesh
        bakedMesh.vertices = vertices;
        bakedMesh.RecalculateNormals();
        skinnedMeshRenderer.sharedMesh = bakedMesh; // Update Skinned Mesh Renderer
    }

    void ApplyForceAtPoint(Vector3 point, Vector3[] vertices)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldVertex = transform.TransformPoint(vertices[i]);
            float distance = Vector3.Distance(worldVertex, point);

            if (distance < radius) // Affect nearby vertices
            {
                Vector3 forceDirection = (worldVertex - point).normalized;
                vertices[i] += forceDirection * forceAmount * (1f - (distance / radius));
            }
        }
    }
}
