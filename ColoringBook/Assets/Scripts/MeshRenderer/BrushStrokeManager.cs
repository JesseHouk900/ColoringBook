using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BrushStrokeManager : MonoBehaviour
{
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Color> colors = new List<Color>();

    private Mesh mesh;
    private MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        mesh = meshFilter.mesh;
    }

    public void AddBrushStroke(List<Vector3> strokeVertices, List<int> strokeTriangles, Color strokeColor)
    {
        int vertexOffset = vertices.Count;

        vertices.AddRange(strokeVertices);
        triangles.AddRange(strokeTriangles.Select(t => t + vertexOffset));
        colors.AddRange(Enumerable.Repeat(strokeColor, strokeVertices.Count));

        UpdateMesh();
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetColors(colors);
    }
}