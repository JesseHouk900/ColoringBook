
using UnityEngine;

public class TextureBrush : Brush
{
    public int textureWidth = 1024;
    public int textureHeight = 768;
    public int brushSize = 5;
    public Color[] brushPixels;
    private bool isDrawing;

    public Transform transform;

    public TextureBrush()
    {
    }
    private void Start()
    {
        // Initialize the texture and brush
        // InitializeTexture();
        InitializeBrush();
        transform = GetComponent<Transform>();
    }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         isDrawing = true;
    //         Draw(Input.mousePosition, Camera.main, null);
    //     }
    //     if (Input.GetMouseButtonUp(0))
    //     {
    //         isDrawing = false;
    //     }
    //     if (isDrawing)
    //     {
    //         Draw(Input.mousePosition, Camera.main, null);
    //     }
    // }

    private void InitializeBrush()
    {
        brushPixels = new Color[brushSize * brushSize];
        for (int i = 0; i < brushPixels.Length; i++)
        {
            brushPixels[i] = brushColor;
            //Debug.Log($"brushPixels: {brushPixels[i]}");
        }
    }

    public override Line Draw(Vector2 position, Camera cam, RenderTexture renderTexture)
    {
        // Vector3 worldPos = cam.ScreenToWorldPoint(position);
        // Debug.Log(transform);
        // Vector3 localPos = transform.InverseTransformPoint(worldPos);
        // int x = Mathf.RoundToInt(localPos.x + textureWidth / 2f - brushSize / 2f);
        // int y = Mathf.RoundToInt(localPos.y + textureHeight / 2f - brushSize / 2f);
        int x = Mathf.RoundToInt(position[0]);
        int y = Mathf.RoundToInt(position[1]);
        Texture2D texture = GameObject.Find("DrawingCanvas").GetComponent<DrawingCanvas>().texture;
        if (x >= 0 && x < textureWidth && y >= 0 && y < textureHeight)
        {
            Debug.Log($@"x: {x} 
                         y: {y}");
            texture.SetPixels(x, y, brushSize, brushSize, brushPixels);
            texture.Apply();
        }

        Refresh?.Invoke();
        return null;
    }

    public override Line DrawLine(Vector2 startPosition, Vector2 endPosition, Camera cam, RenderTexture renderTexture)
    {
        // Implement line drawing logic here if needed
        // For now, simply call Draw for the start position
        return Draw(startPosition, cam, renderTexture);
    }
}
