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
        this.path.Add(new Vector2(UtilsClass.GetMouseWorldPosition().x, UtilsClass.GetMouseWorldPosition().y));
        Awake();

    }
    public BrushStroke(Color _color, Material _material)
    {
        this.brush = new CircleBrush();
        this.brush.brushColor = _color;
        this.brush.material = _material;
        this.path = new List<Vector2>();
        this.path.Add(new Vector2(UtilsClass.GetMouseWorldPosition().x, UtilsClass.GetMouseWorldPosition().y));
        Awake();

    }
    public void Awake()
    {
        //brush = GameObject.Find("Cursor").GetComponent<GameController>().brush;
    }

    public List<Vector3> GetVertices()
    {
        return new List<Vector3>(stroke.vertices);
    }
    public List<int> GetTriangles()
    {
        return new List<int>(stroke.triangles);
    }



    public void CreateMeshBrush()
    {
        stroke = new Mesh();
        //Debug.Log("Mouse Position " + UtilsClass.GetMouseWorldPosition());
        //OnChangeColor(Color.red);

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = UtilsClass.GetMouseWorldPosition();
        vertices[1] = UtilsClass.GetMouseWorldPosition();
        vertices[2] = UtilsClass.GetMouseWorldPosition();
        vertices[3] = UtilsClass.GetMouseWorldPosition();

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


        lastMousePosition = UtilsClass.GetMouseWorldPosition();
        path.Add(lastMousePosition);
    }

    public void CreateMeshBrushStroke()
    {
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

        Vector3 mouseForwardPosition = (UtilsClass.GetMouseWorldPosition() - lastMousePosition).normalized;
        Vector3 normal2D = new Vector3(0, 0, -1f);
        lineThickness = 1f;
        Vector3 newUpPosition = UtilsClass.GetMouseWorldPosition() + Vector3.Cross(mouseForwardPosition, normal2D) * lineThickness;
        Vector3 newDownPosition = UtilsClass.GetMouseWorldPosition() + Vector3.Cross(mouseForwardPosition, normal2D * -1f) * lineThickness;

        vertices[vIndex2] = newUpPosition;
        vertices[vIndex3] = newDownPosition;

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

        lastMousePosition = UtilsClass.GetMouseWorldPosition();
    }


    public void EndMeshBrushStroke()
    {
        isActive = false;
        Debug.Log("End Brush Stroke");
    }

    public void OnChangeColor(Color newColor)
    {
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
}