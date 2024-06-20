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

    /// <summary>
    /// The RenderTexture used to display the canvas.
    /// </summary>
    public RenderTexture renderTexture;

    /// <summary>
    /// The RawImage used to display the RenderTexture.
    /// </summary>
    public RawImage displayImage;

    /// <summary>
    /// The current brush used for drawing.
    /// </summary>
    [SerializeField] private Brush currentBrush;
    // [SerializeField] private Transform debugVisual1;
    // [SerializeField] private Transform debugVisual2;

    private bool isLoaded = false;
    private void Awake()
    {
        // Initialize the brush with a default brush
        Debug.Log("Brushing");
        currentBrush = Instantiate(GetComponent<BrushButton>().brush);
        currentBrush.Refresh = Refresh;
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
        displayImage.texture = renderTexture;

        // // Initialize the brush with a default brush
        // currentBrush = new CircleBrush();
        // currentBrush.Refresh = Refresh;

        // Set Event listeners for being drawn on
        GetComponent<MouseEventHandler>().OnClick += OnClick;
        GetComponent<MouseEventHandler>().OnDrag += OnDrag;
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
        return currentBrush;
    }

    /// <summary>
    /// Called when the user clicks on the canvas.
    /// </summary>
    public void OnClick()
    {
        lastMousePosition = null; // Reset lastMousePosition when the user clicks
        DrawBrushStroke(GameObject.Find("Cursor").GetComponent<CursorMovement>().transform.position);
        //Debug.Log("Click");
    }

    /// <summary>
    /// Called when the user drags on the canvas.
    /// </summary>
    public void OnDrag()
    {
        DrawBrushStroke(GameObject.Find("Cursor").GetComponent<CursorMovement>().transform.position);
        //Debug.Log("Drag");
    }

    /// <summary>
    /// Changes the current brush used for drawing.
    /// </summary>
    /// <param name="newBrush">The new brush to use.</param>
    public void ChangeBrush(Brush newBrush, Color newColor)
    {
        currentBrush = newBrush;
        currentBrush.brushColor = newColor;
    }

    private Vector2? lastMousePosition; // Make lastMousePosition nullable

    /// <summary>
    /// Draws a brush stroke at the specified position.
    /// </summary>
    /// <param name="position">The position to draw at.</param>
    public void DrawBrushStroke(Vector2 position)
    {
        // Use the current brush to draw
        if (lastMousePosition.HasValue) // Check if lastMousePosition has a value
        {
            // Draw a line from the last position to the current position
            currentBrush.DrawLine(lastMousePosition.Value, position, cam, renderTexture);
        }

        lastMousePosition = position;
    }

    /// <summary>
    /// Refreshes the canvas by re-rendering it.
    /// </summary>
    public void Refresh()
    {
        cam.targetTexture = renderTexture;
        cam.Render();
        cam.targetTexture = null;
    }
}