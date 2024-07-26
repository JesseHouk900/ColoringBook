using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushStroke : MonoBehaviour
{
    private float lineThickness;
    private float minDistance;
    public Vector3 lastMousePosition;
    public bool isActive;
    private Mesh stroke;
    public Color color;
    public Material material;
    //public float strokeThickness = 1f;

    public Brush brush;
    public List<Vector2> path;

    public int PathIndex {
        get {
            // Check that the path is not null before trying to access its length
            Debug.Assert(path != null, "Path is null");
            if (!isIndexChosen) 
            {
                return path.Count - 1;
            }
            else 
            {
                // Check that the chosen index is within the bounds of the path
                Debug.Assert(pathIndex >= 0 && pathIndex < path.Count, "Path index is out of range");
                return pathIndex;
            }
        }
        set {
            // Check that the new index is within the bounds of the path
            Debug.Assert(value >= 0 && value < path.Count, "New path index is out of range");
            isIndexChosen = true;
            pathIndex = value;
        }
    }
    private int pathIndex = 0;

    public bool isIndexChosen = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="brush"></param>
    /// <param name="path"></param>
    public BrushStroke(Brush brush, List<Vector2> path)
    {
        this.brush = brush;
        this.path = path;
    }

    public BrushStroke(Color _color)
    {
        this.brush = new CircleBrush();
        this.brush.brushColor = _color;
        this.path = new List<Vector2>();
        this.path.Add(new Vector2(Utils.GetMouseWorldPosition().x, Utils.GetMouseWorldPosition().y));
        Awake();

    }
    public BrushStroke(Color _color, Material _material)
    {
        this.brush = new CircleBrush();
        this.brush.brushColor = _color;
        this.brush.material = _material;
        this.path = new List<Vector2>();
        this.path.Add(new Vector2(Utils.GetMouseWorldPosition().x, Utils.GetMouseWorldPosition().y));
        Awake();

    }
    public void Awake()
    {
        //brush = GameObject.Find("Cursor").GetComponent<GameController>().brush;
    }

    public List<Vector3> GetVertices()
    {
        // Check that the stroke mesh is not null before trying to access its vertices
        Debug.Assert(stroke != null, "Stroke mesh is null");
        return new List<Vector3>(stroke.vertices);
    }

    public List<int> GetTriangles()
    {
        // Check that the stroke mesh is not null before trying to access its triangles
        Debug.Assert(stroke != null, "Stroke mesh is null");
        return new List<int>(stroke.triangles);

    }

    public void CreateMeshBrush()
    {
        // Check that the path is not null or empty before trying to create a mesh brush
        Debug.Assert(path != null && path.Count > 0, "Path is null or empty");

        stroke = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = GetCurrentPosition();
        vertices[1] = GetCurrentPosition();
        vertices[2] = GetCurrentPosition();
        vertices[3] = GetCurrentPosition();

        uv[0] = Vector2.zero;
        uv[1] = Vector2.zero;
        uv[2] = Vector2.zero;
        uv[3] = Vector2.zero;

        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;

        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        stroke.vertices = vertices;
        stroke.uv = uv;
        stroke.triangles = triangles;
        stroke.MarkDynamic();
        //FindObjectsOfType<GameController>()[0].brush.GetComponent<MeshFilter>().mesh = stroke;

        lastMousePosition = GetCurrentPosition();
        path.Add(lastMousePosition);
    }

    public void CreateMeshBrushStroke()
    {
        // Check that the stroke mesh is not null before trying to modify it
        Debug.Assert(stroke != null, "Stroke mesh is null");
        // Check that the path index is at least 1 before trying to create a mesh brush stroke
        Debug.Assert(PathIndex >= 1, "Path index is too small");

        Vector3[] vertices = new Vector3[stroke.vertices.Length + 2];
        Vector2[] uv = new Vector2[stroke.uv.Length + 2];
        int[] triangles = new int[stroke.triangles.Length + 6];
        //Debug.Log("Before: " + FindObjectsOfType<GameController>()[0].brush.GetComponent<MeshFilter>().sharedMesh.vertices.Length);

        stroke.vertices.CopyTo(vertices, 0);
        stroke.uv.CopyTo(uv, 0);
        stroke.triangles.CopyTo(triangles, 0);

        int vIndex = vertices.Length - 4;
        int vIndex0 = vIndex + 0;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        Vector3 mouseForwardPosition = (GetCurrentPosition() - new Vector3(path[PathIndex - 1].x, path[PathIndex - 1].y, 0)).normalized;

        Vector3 normal2D = new Vector3(0, 0, -1f);
        lineThickness = 1f;
        Vector3 newUpPosition = GetCurrentPosition() + Vector3.Cross(mouseForwardPosition, normal2D) * lineThickness;
        Vector3 newDownPosition = GetCurrentPosition() + Vector3.Cross(mouseForwardPosition, normal2D * -1f) * lineThickness;
        vertices[vIndex2] = newUpPosition;
        vertices[vIndex3] = newDownPosition;

        // vertices[vIndex2] = newUpPosition;
        // vertices[vIndex3] = newDownPosition;

        //Debug.Log("Stroke: " + newUpPosition + ", " + newDownPosition);
        uv[vIndex2] = Vector2.zero;
        uv[vIndex3] = Vector2.zero;

        int tIndex = triangles.Length - 6;

        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex2;
        triangles[tIndex + 2] = vIndex1;

        triangles[tIndex + 3] = vIndex1;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;

        stroke.vertices = vertices;
        stroke.uv = uv;
        stroke.triangles = triangles;
        stroke.MarkDynamic();

    lastMousePosition = GetCurrentPosition();
    }

    public void EndMeshBrushStroke()
    {
        // Check that the stroke is active before trying to end it
        Debug.Assert(isActive, "Stroke is not active");

        isActive = false;
        Debug.Log("End Brush Stroke");
    }

    public void OnChangeColor(Color newColor)
    {
        // Check that the new color is not null before trying to set it
        Debug.Assert(newColor != null, "New color is null");
 
        Debug.Log("newColor: " + newColor);
        if (newColor != null && newColor.a != 0)
        {
            SetColor(newColor);
        }
    }

    private void SetColor(Color newColor)
    {
        //Debug.Log("Material: " + material.GetColor("_EmissionColor"));
        // var meshRenderer = GetComponent<MeshRenderer>();
        // Debug.Log("MeshRenderers materials: " + meshRenderer.materials[0].ToString());
        // var _material = meshRenderer.materials[0];
        // material = new Material(_material);
        // material.SetColor("_EmissionColor", newColor);
        //meshRenderer.materials = materials;
        color = newColor;
    }
    public Mesh GetMesh()
    {
        return stroke;
    }
    private Vector3 GetCurrentPosition()
    {
        return new Vector3(path[PathIndex].x, path[PathIndex].y, 0);
    }
}