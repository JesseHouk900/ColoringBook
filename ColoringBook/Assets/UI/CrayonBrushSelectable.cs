using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrayonBrushSelectable : MonoBehaviour
{
    public CrayonBrushSelectable brushSelector;
    public Color color;

    public void Awake()
    {
        brushSelector = GetComponent<CrayonBrushSelectable>();
    }
    public void OnClick(GameObject cursor)
    {
        CrayonMeshRenderer meshRenderer = (CrayonMeshRenderer)FindObjectsOfType(typeof(CrayonMeshRenderer))[0];
        Debug.Log(meshRenderer.GetType());
        meshRenderer.OnChangeColor(color);
        // var brush = cursor.GetComponent<GameController>().brush;
        // var meshRenderer = brush.GetComponent<MeshRenderer>();
        // Debug.Log(brush.GetType());
        //Debug.Log();
    }
}