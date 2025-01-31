using UnityEngine;

public class Jellyfier : MonoBehaviour
{
    public float bounceSpeed;
    public float fallForce;
    public float stiffness;
    
    private MeshFilter meshFilter;
    private Mesh mesh;

    private JellyVertex[] jellyVertices;
    Vector3[] currentMeshVertices;

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;

        GetVertices();
    }

    private void GetVertices()
    {
        jellyVertices = new JellyVertex[mesh.vertices.Length];
        currentMeshVertices = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            jellyVertices[i] = new JellyVertex(i, mesh.vertices[i], mesh.vertices[i], Vector3.zero);
            currentMeshVertices[i] = mesh.vertices[i];
        }
    }

    private void Update()
    {
        UpdateVertices();
    }

    private void UpdateVertices()
    {
        for (int i = 0; i < jellyVertices.Length; i++)
        {
            jellyVertices[i].UpdateVelocity(bounceSpeed);
            jellyVertices[i].Settle(stiffness);
            
            jellyVertices[i].currentVertexPosition += jellyVertices[i].currentVelocity * Time.deltaTime;
            currentMeshVertices[i] = jellyVertices[i].currentVertexPosition;
            
        }
        mesh.vertices = currentMeshVertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    public void OnCollisionEnter(Collision other)
    {
        ContactPoint[] collisonPoints = other.contacts;
        for (int i = 0; i < collisonPoints.Length; i++)
        {
            Vector3 inputPoint = collisonPoints[i].point + (collisonPoints[i].point * 1f);
            ApplyPressureToPoint(inputPoint, fallForce);
        }
    }

    public void ApplyPressureToPoint(Vector3 _point, float _pressure)
    {
        for (int i = 0; i < jellyVertices.Length; i++)
        {
            jellyVertices[i].ApplyPressureToVertex(transform, _point, _pressure);
        }
    }
}
