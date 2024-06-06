using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLineRenderer : MonoBehaviour
{
    public GameObject brush;
    public LineRenderer currentLineRenderer;

    public Vector2 lastPos;

    private Vector4 color;

    virtual public void Update() {}

    virtual public void Draw() {}

    virtual public void CreateBrush() {}

    virtual public void AddAPoint(Vector2 pointPos) {}
    virtual public void SetBrush() {}

}
