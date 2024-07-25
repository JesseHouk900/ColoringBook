// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MeshBrush : Brush
// {
//     private float lineThickness;
//     private float minDistance;
//     public Vector3 lastMousePosition;
//     public bool isActive;
//     private MeshLine stroke;
//     public Color color;
//     public Material material;
//     //public float strokeThickness = 1f;

//     public Brush brush;
//     public List<Vector2> path;

//     public int PathIndex {
//         get {
//             // Check that the path is not null before trying to access its length
//             Debug.Assert(path != null, "Path is null");
//             if (!isIndexChosen) 
//             {
//                 return path.Count - 1;
//             }
//             else 
//             {
//                 // Check that the chosen index is within the bounds of the path
//                 Debug.Assert(pathIndex >= 0 && pathIndex < path.Count, "Path index is out of range");
//                 return pathIndex;
//             }
//         }
//         set {
//             // Check that the new index is within the bounds of the path
//             Debug.Assert(value >= 0 && value < path.Count, "New path index is out of range");
//             isIndexChosen = true;
//             pathIndex = value;
//         }
//     }
//     private int pathIndex = 0;

//     public bool isIndexChosen = false;
//     GameObject lastCircle;
//     public MeshBrush()
//     {

//     }

//     public MeshBrush(Brush _brush)
//     {
//         Refresh = _brush.Refresh;
//         brushColor = _brush.brushColor;
//         material = _brush.material;
        
//     }

//     public MeshBrush(Brush brush, List<Vector2> path)
//     {
//         this.brush = brush;
//         this.path = path;
//     }

//     public MeshBrush(Color _color)
//     {
//         this.brush = new CircleBrush();
//         this.brush.brushColor = _color;
//         this.path = new List<Vector2>();
//         this.path.Add(new Vector2(UtilsClass.GetMouseWorldPosition().x, UtilsClass.GetMouseWorldPosition().y));
//         Awake();

//     }
//     public MeshBrush(Color _color, Material _material)
//     {
//         this.brush = new CircleBrush();
//         this.brush.brushColor = _color;
//         this.brush.material = _material;
//         this.path = new List<Vector2>();
//         this.path.Add(new Vector2(UtilsClass.GetMouseWorldPosition().x, UtilsClass.GetMouseWorldPosition().y));
//         Awake();

//     }
//     public void Awake()
//     {
//         //brush = GameObject.Find("Cursor").GetComponent<GameController>().brush;
//     }

//     public List<Vector3> GetVertices()
//     {
//         // Check that the stroke mesh is not null before trying to access its vertices
//         Debug.Assert(stroke != null, "Stroke mesh is null");
//         return new List<Vector3>(stroke.mesh.vertices);
//     }

//     public List<int> GetTriangles()
//     {
//         // Check that the stroke mesh is not null before trying to access its triangles
//         Debug.Assert(stroke != null, "Stroke mesh is null");
//         return new List<int>(stroke.mesh.triangles);

//     }

//     public Line CreateMeshBrush()
//     {
//         // Check that the path is not null or empty before trying to create a mesh brush
//         Debug.Assert(path != null && path.Count > 0, "Path is null or empty");
//         stroke = new MeshLine();
//         stroke.mesh = new Mesh();

//         Vector3[] vertices = new Vector3[4];
//         Vector2[] uv = new Vector2[4];
//         int[] triangles = new int[6];

//         vertices[0] = GetCurrentPosition();
//         vertices[1] = GetCurrentPosition();
//         vertices[2] = GetCurrentPosition();
//         vertices[3] = GetCurrentPosition();

//         uv[0] = Vector2.zero;
//         uv[1] = Vector2.zero;
//         uv[2] = Vector2.zero;
//         uv[3] = Vector2.zero;

//         triangles[0] = 0;
//         triangles[1] = 2;
//         triangles[2] = 1;

//         triangles[3] = 1;
//         triangles[4] = 3;
//         triangles[5] = 2;

//         stroke.mesh.vertices = vertices;
//         stroke.mesh.uv = uv;
//         stroke.mesh.triangles = triangles;
//         stroke.mesh.MarkDynamic();
//         //FindObjectsOfType<GameController>()[0].brush.GetComponent<MeshFilter>().mesh = stroke;

//         lastMousePosition = GetCurrentPosition();
//         path.Add(lastMousePosition);

//         return stroke;
//     }

//     public Line CreateMeshBrushStroke()
//     {
//         // Check that the stroke mesh is not null before trying to modify it
//         Debug.Assert(stroke != null, "Stroke mesh is null");
//         // Check that the path index is at least 1 before trying to create a mesh brush stroke
//         Debug.Assert(PathIndex >= 1, "Path index is too small");

//         Vector3[] vertices = new Vector3[stroke.mesh.vertices.Length + 2];
//         Vector2[] uv = new Vector2[stroke.mesh.uv.Length + 2];
//         int[] triangles = new int[stroke.mesh.triangles.Length + 6];
//         //Debug.Log("Before: " + FindObjectsOfType<GameController>()[0].brush.GetComponent<MeshFilter>().sharedMesh.vertices.Length);

//         stroke.mesh.vertices.CopyTo(vertices, 0);
//         stroke.mesh.uv.CopyTo(uv, 0);
//         stroke.mesh.triangles.CopyTo(triangles, 0);

//         int vIndex = vertices.Length - 4;
//         int vIndex0 = vIndex + 0;
//         int vIndex1 = vIndex + 1;
//         int vIndex2 = vIndex + 2;
//         int vIndex3 = vIndex + 3;

//         Vector3 mouseForwardPosition = (GetCurrentPosition() - new Vector3(path[PathIndex - 1].x, path[PathIndex - 1].y, 0)).normalized;

//         Vector3 normal2D = new Vector3(0, 0, -1f);
//         lineThickness = 1f;
//         Vector3 newUpPosition = GetCurrentPosition() + Vector3.Cross(mouseForwardPosition, normal2D) * lineThickness;
//         Vector3 newDownPosition = GetCurrentPosition() + Vector3.Cross(mouseForwardPosition, normal2D * -1f) * lineThickness;
//         vertices[vIndex2] = newUpPosition;
//         vertices[vIndex3] = newDownPosition;

//         // vertices[vIndex2] = newUpPosition;
//         // vertices[vIndex3] = newDownPosition;

//         //Debug.Log("Stroke: " + newUpPosition + ", " + newDownPosition);
//         uv[vIndex2] = Vector2.zero;
//         uv[vIndex3] = Vector2.zero;

//         int tIndex = triangles.Length - 6;

//         triangles[tIndex + 0] = vIndex0;
//         triangles[tIndex + 1] = vIndex2;
//         triangles[tIndex + 2] = vIndex1;

//         triangles[tIndex + 3] = vIndex1;
//         triangles[tIndex + 4] = vIndex2;
//         triangles[tIndex + 5] = vIndex3;

//         stroke.mesh.vertices = vertices;
//         stroke.mesh.uv = uv;
//         stroke.mesh.triangles = triangles;
//         stroke.mesh.MarkDynamic();

//         lastMousePosition = GetCurrentPosition();
//         return stroke;
//     }

//     public void EndMeshBrushStroke()
//     {
//         // Check that the stroke is active before trying to end it
//         Debug.Assert(isActive, "Stroke is not active");

//         isActive = false;
//         Debug.Log("End Brush Stroke");
//     }

//     // public void OnChangeColor(Color newColor)
//     // {
//     //     // Check that the new color is not null before trying to set it
//     //     Debug.Assert(newColor != null, "New color is null");
 
//     //     Debug.Log("newColor: " + newColor);
//     //     if (newColor != null && newColor.a != 0)
//     //     {
//     //         SetColor(newColor);
//     //     }
//     // }

//     public Mesh GetMesh()
//     {
//         return stroke.mesh;
//     }
//     private Vector3 GetCurrentPosition()
//     {
//         return new Vector3(path[PathIndex].x, path[PathIndex].y, 0);
//     }

//     public override Line Draw(Vector2 position, Camera cam, RenderTexture renderTexture)
//     {
//         return CreateMeshBrush();
//     }

//     public override Line DrawLine(Vector2 startPosition, Vector2 endPosition, Camera cam, RenderTexture renderTexture)
//     {
//         return CreateMeshBrushStroke();
//     }
// }