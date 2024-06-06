using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBrush : Brush
{
    public override void Draw(Vector2 position, Camera cam, RenderTexture renderTexture)
    {
        // Create a circle game object
        GameObject circle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        circle.transform.position = position;

        // Set the color of the circle
        circle.GetComponent<Renderer>().material.color = brushColor;

        // Render the circle onto the RenderTexture
        Refresh();
    }

    public override void DrawLine(Vector2 startPosition, Vector2 endPosition, Camera cam, RenderTexture renderTexture)
    {
        // Calculate the direction from the start to the end
        Vector2 direction = (endPosition - startPosition).normalized;

        // Calculate the distance from the start to the end
        float distance = Vector2.Distance(startPosition, endPosition);

        // Draw circles along the line
        for (float i = 0; i < distance; i++)
        {
            Vector2 position = startPosition + direction * i;
            GameObject circle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            circle.transform.position = position;

            // Set the color of the circle
            circle.GetComponent<Renderer>().material.color = brushColor;

            // Render the circle onto the RenderTexture
            Refresh();
        }
    }
}