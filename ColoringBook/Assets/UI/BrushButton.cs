using UnityEngine;
using UnityEngine.UI;

public class BrushButton : MonoBehaviour
{
    public Brush brush;
    public Color brushColor; // Add a color field for the brush

    public BrushButton(Brush _brush, Color _color)
    {
        brush = _brush;
        brushColor = _color;
    }
    public void OnClick()
    {
        Debug.Log(brushColor);
        Debug.Log("CLicked");
        // Assuming you have a reference to the DrawingCanvas
        GameObject.Find("DrawingCanvas").GetComponent<DrawingCanvas>().ChangeBrush(brush, brushColor);
    }
}