using UnityEngine;

// Base class for all brushes
public abstract class Brush : MonoBehaviour
{
    public delegate void RefreshEventHandlerDelegate();
    public RefreshEventHandlerDelegate Refresh;
    public Color brushColor;
    public Material material;

    public abstract void Draw(Vector2 position, Camera cam, RenderTexture renderTexture);
    public abstract void DrawLine(Vector2 startPosition, Vector2 endPosition, Camera cam, RenderTexture renderTexture);
}