using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBrush : Brush
{

    public CircleBrush()
    {

    }

    public CircleBrush(Brush _brush)
    {
        Refresh = _brush.Refresh;
        brushColor = _brush.brushColor;
        material = _brush.material;
        
    }
    private Line currentLine;
    private GameObject lastCircle;

    public override Line Draw(Vector2 position, Camera cam, RenderTexture renderTexture)
    {
        // Create the first point of the line
        if (currentLine == null)
        {
            currentLine = new Line(brushColor, this);
        }

        // Create a circle game object
        GameObject circle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        circle.transform.position = position;
        circle.transform.position = new Vector3(circle.transform.position.x, circle.transform.position.y, 0); // Ensure Z-value is 0 for new points

        // Set the color of the circle
        circle.GetComponent<Renderer>().material.color = brushColor;

        // Set this circle as the last circle made
        lastCircle = circle;

        // Add this position to the current line
        currentLine.points.Add(position);

        // Render the circle onto the RenderTexture
        Refresh();

        return currentLine;
    }

    public override Line DrawLine(Vector2 startPosition, Vector2 endPosition, Camera cam, RenderTexture renderTexture)
    {
        Debug.Log("Drawing when I shouldn't be");
        // If we're calling DrawLine directly, we just continue the current line
        // Otherwise, we assume Draw has already added the first point
        if (startPosition != endPosition)
        {
            Vector2 direction = (endPosition - startPosition).normalized;
            float distance = Vector2.Distance(startPosition, endPosition);

            for (float i = 1; i < distance; i++) // Start from 1 since 0 is already added by Draw()
            {
                Vector2 newPosition = startPosition + direction * i;
                Draw(newPosition, cam, renderTexture);
            }

            // Add the end position as well
            Draw(endPosition, cam, renderTexture);
        }

        // At this point, we've finished drawing the line, so we can return it
        return currentLine;
    }
    // public override Line Draw(Vector2 position, Camera cam, RenderTexture renderTexture)
    // {
    //     // Create a circle game object
    //     GameObject circle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //     circle.transform.position = position;

    //     // Set the color of the circle
    //     circle.GetComponent<Renderer>().material.color = brushColor;

    //     // Set first circle as last circle made
    //     lastCircle = circle;

    //     // Render the circle onto the RenderTexture
    //     Refresh();
    //     List<Vector2> points = new List<Vector2>();
    //     points.Add(position);
    //     Line line = new Line(points, brushColor, this);
    //     lines.Add(line);
    //     return line;
    // }

    // private List<Line> lines = new List<Line>();
    // private float currentDepth = 0;

    // public override Line DrawLine(Vector2 startPosition, Vector2 endPosition, Camera cam, RenderTexture renderTexture)
    // {
    //     // Calculate the direction from the start to the end
    //     Vector2 direction = (endPosition - startPosition).normalized;

    //     // Calculate the distance from the start to the end
    //     float distance = Vector2.Distance(startPosition, endPosition);

    //     // Create initial circle
    //     GameObject circle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //     // Set Start position
    //     circle.transform.position = startPosition;
    //     // Set the color of the circle
    //     circle.GetComponent<Renderer>().material.color = brushColor;

    //     // Set last circle
    //     lastCircle = circle;

    //     List<Vector2> points = new List<Vector2>();
    //     points.Add(startPosition);

    //     // Draw circles along the line
    //     for (float i = 0; i < distance; i++)
    //     {
    //         Vector2 position = startPosition + direction * i;
    //         circle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //         circle.transform.position = new Vector3(position.x, position.y, currentDepth);

    //         // Set the color of the circle
    //         circle.GetComponent<Renderer>().material.color = brushColor;

    //         points.Add(position);

    //         // Get the last circles position
    //         Vector3 lastPosition = lastCircle.transform.position;
    //         // Move last circle to the background
    //         lastCircle.transform.position = new Vector3(lastPosition.x, lastPosition.y, currentDepth - 1);
    //         // Set the last circle to the most recently made circle
    //         lastCircle = circle;

    //         // Render the circle onto the RenderTexture
    //         //Refresh();
    //     }

    //     Line line = new Line(points, brushColor, this);
    //     lines.Add(line);

    //     currentDepth++;

    //     return line;
    // }

}