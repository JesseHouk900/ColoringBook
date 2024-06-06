// using UnityEngine;
// using UnityEngine.UI;

// /// 

// /// A script that handles drawing on a canvas using a brush.
// /// 

// public class DrawingCanvas : MonoBehaviour
// {
//     /// 

//     /// The width of the canvas.
//     /// 

//     public int width = 512;

//     /// 

//     /// The height of the canvas.
//     /// 

//     public int height = 512;

//     /// 

//     /// The camera used to render the canvas.
//     /// 

//     public Camera cam;

//     /// 

//     /// The RenderTexture used to display the canvas.
//     /// 

//     public RenderTexture renderTexture;

//     /// 

//     /// The RawImage used to display the RenderTexture.
//     /// 

//     public RawImage displayImage;

//     /// 

//     /// The current brush used for drawing.
//     /// 

//     private Brush currentBrush;

//     private void Start()
//     {
//         // Set the default image size to the screen size
//         SetDefaultImageSize();

//         // Create a new RenderTexture and display it on the RawImage
//         renderTexture = new RenderTexture(width, height, 0);
//         renderTexture.Create();
//         cam.targetTexture = renderTexture;
//         cam.Render();
//         cam.targetTexture = null;
//         displayImage.texture = renderTexture;

//         // Initialize the brush with a default brush
//         currentBrush = new CircleBrush();
//         currentBrush.Refresh = Refresh;

//         // Set Event listeners for being drawn on
//         GetComponent<MouseEventHandler>().OnClick += OnClick;
//         GetComponent<MouseEventHandler>().OnDrag += OnDrag;
//     }

//     /// 

//     /// Sets the default image size to the screen size.
//     /// 

//     private void SetDefaultImageSize()
//     {
//         width = Screen.width;
//         height = Screen.height;
//     }

//     /// 

//     /// Called when the user clicks on the canvas.
//     /// 

//     public void OnClick()
//     {
//         lastMousePosition = null; // Reset lastMousePosition when the user clicks
//         DrawBrushStroke(GameObject.Find("Cursor").GetComponent().transform.position);
//         Debug.Log("Click");
//     }

//     /// 

//     /// Called when the user drags on the canvas.
//     /// 

//     public void OnDrag()
//     {
//         DrawBrushStroke(GameObject.Find("Cursor").GetComponent().transform.position);
//         Debug.Log("Drag");
//     }

//     /// 

//     /// Changes the current brush used for drawing.
//     /// 

//     /// The new brush to use.
//     public void ChangeBrush(Brush newBrush)
//     {
//         currentBrush = newBrush;
//     }

//     private Vector2? lastMousePosition; // Make lastMousePosition nullable

//     /// 

//     /// Draws a brush stroke at the specified position.
//     /// 

//     /// The position to draw at.
//     public void DrawBrushStroke(Vector2 position)
//     {
//         // Use the current brush to draw
//         if (lastMousePosition.HasValue) // Check if lastMousePosition has a value
//         {
//             // Draw a line from the last position to the current position
//             currentBrush.DrawLine(lastMousePosition.Value, position, cam, renderTexture);
//         }

//         lastMousePosition = position;
//     }

//     /// 

//     /// Refreshes the canvas by re-rendering it.
//     /// 

//     public void Refresh()
//     {
//         cam.targetTexture = renderTexture;
//         cam.Render();
//         cam.targetTexture = null;
//     }
// }