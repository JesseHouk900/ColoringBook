using UnityEngine;

// Base class for all brushes
public abstract class Brush : MonoBehaviour
{
    public delegate void RefreshEventHandlerDelegate();
    public RefreshEventHandlerDelegate Refresh;
    public Color brushColor;
    public Material material;

    public abstract Line Draw(Vector2 position, Camera cam, RenderTexture renderTexture);
    public abstract Line DrawLine(Vector2 startPosition, Vector2 endPosition, Camera cam, RenderTexture renderTexture);
}