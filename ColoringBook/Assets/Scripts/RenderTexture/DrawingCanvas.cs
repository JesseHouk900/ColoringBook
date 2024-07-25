using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script that handles drawing on a canvas using a brush.
/// </summary>
public class DrawingCanvas : MonoBehaviour
{
    /// <summary>
    /// The width of the canvas.
    /// </summary>
    public int width = 512;

    /// <summary>
    /// The height of the canvas.
    /// </summary>
    public int height = 512;

    /// <summary>
    /// The camera used to render the canvas.
    /// </summary>
    public Camera cam;
    public Texture2D texture;
    private Sprite drawSprite;

    /// <summary>
    /// The RenderTexture used to display the canvas.
    /// </summary>
    public RenderTexture renderTexture;

    /// <summary>
    /// The RawImage used to display the RenderTexture.
    /// </summary>
    public RawImage displayImage;

    /// <summary>
    /// 
    /// </summary>
    public GameObject brushPrefab;

    /// <summary>
    /// The current brush used for drawing.
    /// </summary>
    [SerializeField] private GameObject currentBrush;

    // [SerializeField] private Transform debugVisual1;
    // [SerializeField] private Transform debugVisual2;

    private bool isLoaded = false;
    private bool isDrawing = false;
    private void Awake()
    {
        // Initialize the brush with a default brush
        Debug.Log("Brushing");
        //currentBrush = Instantiate(GetComponent<BrushButton>().brush);
        currentBrush = Instantiate(brushPrefab);
        GetBrush().Refresh = Refresh;
    }
    private void Start()
    {
        Debug.Log("Loading");
        // Set the default image size to the screen size
        SetDefaultImageSize();

        // Clear the camera's background color
        cam.clearFlags = CameraClearFlags.SolidColor;

        // Create a new RenderTexture
        renderTexture = new RenderTexture(width, height, 0);
        renderTexture.Create();

        // Set up the camera to render to the RenderTexture
        cam.targetTexture = renderTexture;
        cam.Render();

        // Set the camera back to rendering to the screen
        cam.targetTexture = null;
        // Display the RenderTexture on a RawImage
        //displayImage.texture = renderTexture;
        InitializeTexture();

        // Initialize the brush with a default brush
        // currentBrush = new GameObject("TextureBrush");
        // currentBrush.transform.parent = this.transform;
        Brush brush = GetBrush();
        Debug.Log($"brush: {brush}");

        brush.Refresh = Refresh;
        Debug.Log($@"brush: {brush.GetType()}
                    brushPixels: {((TextureBrush)brush).brushPixels}
                    transform: {((TextureBrush)brush).transform.position}");

        lines = new List<Line>();
        // Set Event listeners for being drawn on
        GetComponent<MouseEventHandler>().OnClick += OnClick;
        GetComponent<MouseEventHandler>().OnDrag += OnDrag;
        GetComponent<MouseEventHandler>().OnRelease += OnRelease;
    }

    private void OnValidate()
    {
        if (!isLoaded)
        {
            //Start();
            isLoaded = true;
        }

    }

    /// <summary>
    /// Sets the default image size to the screen size.
    /// </summary>
    private void SetDefaultImageSize()
    {
        width = Screen.width;
        height = Screen.height;
    }

    /// <summary>
    /// Gets the currently active brush
    /// </summary>
    public Brush GetBrush()
    {
        return currentBrush.GetComponent<Brush>();
    }

    /// <summary>
    /// Called when the user clicks on the canvas.
    /// </summary>
    public void OnClick()
    {
        // Debug.Log($@"brushPixels: {((TextureBrush)currentBrush).brushPixels}
        //             transform: {((TextureBrush)currentBrush).transform.position}");
        lastMousePosition = null; // Reset lastMousePosition when the user clicks
        DrawBrushStroke(GameObject.Find("Cursor").GetComponent<CursorMovement>().transform.position);
        Debug.Log("Click");
    }

    /// <summary>
    /// Called when the user drags on the canvas.
    /// </summary>
    public void OnDrag()
    {
        DrawBrushStroke(GameObject.Find("Cursor").GetComponent<CursorMovement>().transform.position);
        //Debug.Log("Drag");
    }

    public void OnRelease()
    {
        EndDrawBrushStroke();
    }

    /// <summary>
    /// Changes the current brush used for drawing.
    /// </summary>
    /// <param name="newBrush">The new brush to use.</param>
    public void ChangeBrush(Brush newBrush, Color newColor)
    {
        Brush brush = GetBrush();
        brush = newBrush;
        brush.brushColor = newColor;
        brush.Refresh = Refresh;
    }


    /// <summary>
    /// Draws a brush stroke at the specified position.
    /// </summary>
    /// <param name="position">The position to draw at.</param>
    private List<Line> lines = new List<Line>();
    private Vector2? lastMousePosition;

    public void DrawBrushStroke(Vector2 position)
    {
        if (lastMousePosition.HasValue)
        {
            if (!isDrawing)
            {
                isDrawing = true;
            }
            Debug.Log(Input.mousePosition);
            // Draw a line from the last position to the current position
            Line line = GetBrush().DrawLine(Input.mousePosition, position, cam, renderTexture);
            Debug.Log($"line: {line}");
            lines.Add(line);

        }

        lastMousePosition = position;
    }

    public void EndDrawBrushStroke()
    {
        if(isDrawing)
        {
            // After drawing the line, adjust the Z-values to handle depth
            FixDepth();
        }
        isDrawing = false;
    }

    private void FixDepth()
    {
        // for (int i = 0; i < lines.Count; i++)
        // {
        //     Line line = lines[i];
        //     int pointIndex = 0;
        //     foreach (Vector2 point in line.points)
        //     {
        //         GameObject circle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //         circle.transform.position = new Vector3(point.x, point.y, -i); // Set Z-value based on line order
        //         Destroy(circle, 0.1f); // Optional: Destroy after some time if not needed
        //         pointIndex++;
        //     }
        // }
    }
    // public void DrawBrushStroke(Vector2 position)
    // {
    //     // Use the current brush to draw
    //     if (lastMousePosition.HasValue) // Check if lastMousePosition has a value
    //     {
    //         // Draw a line from the last position to the current position
    //         lines.Add(currentBrush.DrawLine(lastMousePosition.Value, position, cam, renderTexture));
    //         //lines.Add(currentBrush.DrawLine(lastMousePosition.Value, position, cam, renderTexture));
    //     }

    //     lastMousePosition = position;
    // }

    /// <summary>
    /// Refreshes the canvas by re-rendering it.
    /// </summary>
    public void Refresh()
    {
        cam.targetTexture = renderTexture;
        cam.Render();
        cam.targetTexture = null;
    }

    private void InitializeTexture()
    {
        texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Point;


        drawSprite = Sprite.Create(texture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = drawSprite;
        Debug.Log(transform.localScale);
        ResizeSpriteToScreen();
        Debug.Log(transform.localScale);
        var screenPoint = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 2));
        transform.localPosition = new Vector3(screenPoint.x, screenPoint.y, transform.position.z);
        Color[] pixels = new Color[texture.width * texture.height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.white;
        }
        texture.SetPixels(pixels);
        texture.Apply();

        Camera.main.orthographic = true;
        Debug.Log($"Transform adjusted: {transform.position}");
        //Camera.main.orthographicSize = textureHeight / 2;
    }

	void ResizeSpriteToScreen()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;
        
        transform.localScale = new Vector3(1,1,1);
        
        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;
        Debug.Log($"w: {width} h: {height}");
        
        transform.localScale = new Vector3((float)Screen.width / width, (float)Screen.height / height, 1);
    }
}