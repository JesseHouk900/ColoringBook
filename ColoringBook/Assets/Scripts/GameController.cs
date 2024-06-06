using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public Camera camera;
    public Brush brush;
    public List<BrushStroke> brushStrokes = new List<BrushStroke>();

    //public CrayonMeshRenderer currentMeshRenderer;

    public Vector2 lastPos;

    void Start()
    {

        // Instantiate(brush);
        // currentMeshRenderer = new CrayonMeshRenderer();
        //mouseHandler = GetComponent
        Debug.Log("GC started");
    }

    void Update()
    {
        // Draw();
        //currentMeshRenderer.Update();
    }

    // void Draw() {

    // }
}
