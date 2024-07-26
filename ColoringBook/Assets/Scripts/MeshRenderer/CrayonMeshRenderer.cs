using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrayonMeshRenderer : MonoBehaviour
{


    // [SerializeField] private Transform debugVisual1;
    // [SerializeField] private Transform debugVisual2;

    private BrushStroke stroke;
    private float minDistance;
    public Color color;
    public Material material;
    public MeshRenderer meshRenderer;
    // Prefab Stroke Game Object
    public GameObject p_stroke_go;
    // Stroke Game Object
    public GameObject stroke_go;

    public void OnEnable()
    {
        // GameObject.Find("Panel").GetComponent<MouseEventHandler>().OnClick += OnClick;
        // GameObject.Find("Panel").GetComponent<MouseEventHandler>().OnHold += OnHold;
        // GameObject.Find("Panel").GetComponent<MouseEventHandler>().OnRelease += OnRelease;

        stroke.isActive = true;
    }
    public void OnDisable()
    {
        GameObject.Find("Panel").GetComponent<MouseEventHandler>().OnClick -= OnClick;
        GameObject.Find("Panel").GetComponent<MouseEventHandler>().OnHold -= OnHold;
        GameObject.Find("Panel").GetComponent<MouseEventHandler>().OnRelease -= OnRelease;
        stroke.isActive = false;
    }

    private void Awake()
    {
        color = new Color(0, 1, 0, 1);
        meshRenderer = GetComponent<MeshRenderer>();
        var _material = meshRenderer.materials[0];
        material = new Material(_material);
        stroke = new BrushStroke(color, material);
        OnChangeColor(color);
    }

    private void Update()
    {
        //GetComponent<MeshFilter>().mesh = stroke.GetMesh();
    }

    // On input click
    public void OnClick()
    {
        //Debug.Log("OnClick");
        //OnEnable();
        stroke.CreateMeshBrush();

    }

    // On input being held
    public void OnHold()
    {
        //Debug.Log("OnHold");
        if (Vector3.Distance(stroke.lastMousePosition, Utils.GetMouseWorldPosition()) > minDistance)
        {
            stroke.CreateMeshBrushStroke();
            OnChangeColor(color);
        }

    }

    // On input released
    public void OnRelease()
    {
        //Debug.Log("OnRelease");
        if (stroke.isActive)
        {
            EndMeshBrushStroke();
            OnDisable();
        }
    }

    public void OnChangeColor(Color newColor)
    {
        if (newColor.a != 0)
        {
            SetColor(newColor);
        }
    }
    private void SetColor(Color newColor)
    {
        var materials = meshRenderer.materials;
        materials[0] = new Material(material);
        material.SetColor("_EmissionColor", newColor);
        meshRenderer.materials = materials;
        color = newColor;
    }
    public void EndMeshBrushStroke()
    {
        CreateNewBrushStroke();
        stroke.EndMeshBrushStroke();
    }

    public void CreateNewBrushStroke()
    {
        GetComponent<BrushStrokeManager>().AddBrushStroke(stroke.GetVertices(), stroke.GetTriangles(), stroke.color);
        // AddBrushStrokeToManager();
        // Debug.Log("Strokes: " + GetComponent<BrushStrokeManager>().brushStrokes.Count);
        // stroke = new BrushStroke(color);
    }

    private void AddBrushStrokeToManager()
    {
        // var brushStrokes = GetComponent<BrushStrokeManager>().brushStrokes;
        // brushStrokes.Add(Instantiate(p_stroke_go, transform.position, transform.rotation));
        // stroke_go = brushStrokes[brushStrokes.Count - 1];
        // stroke_go.GetComponent<MeshFilter>().mesh = stroke.GetMesh();
        //stroke_go.GetComponent<BrushStroke>().OnChangeColor(color);
        //stroke_go.GetComponent<MeshFilter>().mesh.transform.parent = stroke.transform.parent;

    }
}