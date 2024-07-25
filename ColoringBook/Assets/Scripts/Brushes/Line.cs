using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public List<Vector2> points = new List<Vector2>();
    public Color color;
    public Brush brush;

    public Line(Color color, Brush brush)
    {
        this.color = color;
        this.brush = brush;
    }
}