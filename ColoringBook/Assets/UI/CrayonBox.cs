using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

/// <summary>
/// This is a <c cref="CrayonBox">CrayonBox</c> class.
/// </summary>
public class CrayonBox : MonoBehaviour
{
    // Array of BrushButton scripts
    public BrushButton[] crayons;

    // Array of colors
    public Color[] colors;

    // Array of color presets
    public ColorPreset[] presets;

    // Prefab for the button
    public GameObject buttonPrefab;

    // Texture for the crayon image
    public Texture2D crayonTexture;

    // Start is called before the first frame update
    private void Start()
    {
        // Load the first preset by default
        LoadPreset(0);

        // Log a message to the console to indicate the CrayonBox has started
        Debug.Log("CrayonBox started");
    }

    // Number of buttons per row
    public int buttonsPerRow = 8;

    // X offset for each column
    public float columnXOffset = 25f;

    // Y offset for each row
    public float yOffset = -25f;

    // This function is called when the script is loaded or a value is changed in the Inspector
    private void OnValidate()
    {
        // Delay the execution of the code until the next frame
        EditorApplication.delayCall += () =>
        {
            // Check if the script is still attached to a game object
            if (this != null)
            {
                // Destroy all child objects of the CrayonBox game object
                foreach (Transform child in transform)
                {
                    DestroyImmediate(child.gameObject);
                }

                // Load the first preset
                LoadPreset(0);
            }
        };
    }

    // Starting position for the buttons
    public Vector3 startPosition;

    // Enum for the direction of the trapezoid
    public enum TrapezoidDirection { Grow, Shrink }

    // Direction of the trapezoid
    public TrapezoidDirection trapezoidDirection = TrapezoidDirection.Shrink;

    // Scale for the buttons
    public Vector3 scale = new Vector3(0.7f, 1f, 1f);

    /// <summary>
    /// This is a <c>LoadPreset</c> method.
    /// </summary>
    /// <param name="index">The index of the preset to load.</param>
    public void LoadPreset(int index)
    {
        // Check if the index is valid
        if (index < 0 || index >= presets.Length)
        {
            // Log an error message to the console
            Debug.LogError("Invalid preset index");
            return;
        }

        // Destroy all child objects of the CrayonBox game object
        // foreach (Transform child in transform)
        // {
        //     Destroy(child.gameObject);
        // }

        // Set the colors to the colors of the preset
        colors = presets[index].colors;

        // Get the current Brush object from the DrawingCanvas
        // var drawingCanvas = GameObject.Find("DrawingCanvas").GetComponent<DrawingCanvas>();
        // var brush = new CircleBrush();
        GameObject brushObject = GameObject.Find("CircleBrush");
        var brush = brushObject.GetComponent<Brush>();
        //var brush = Instantiate(drawingCanvas.GetBrush());

        // Initialize the current position and row offset
        Vector3 currentPosition = startPosition - new Vector3(columnXOffset, 0, 0);
        Debug.Log(startPosition);
        float rowOffset = 0;

        // Initialize the number of buttons per row and the button index
        int buttonsPerRow = this.buttonsPerRow;
        int buttonIndex = 0;

        // Loop through each color in the preset
        for (int i = 0; i < colors.Length; i++)
        {
            // Create a new button
            GameObject buttonObject = Instantiate(buttonPrefab, transform);
            //buttonObject.transform.localPosition = currentPosition;

            buttonObject.transform.localScale = scale;

            // Get the Button component of the button
            Button button = buttonObject.GetComponent<Button>();

            // Set the button's target graphic
            button.targetGraphic = buttonObject.GetComponent<Image>();

            // Create a new BrushButton and attach it to the buttonObject
            BrushButton brushButton = buttonObject.AddComponent<BrushButton>();
            brushButton.brush = brush;
            brushButton.brushColor = colors[i];

            Debug.Assert(brushButton.brush != null && brushButton.brushColor == colors[i], "Brush must not be null and brush color must match the color in the colors array");

            // Set the button's texture and color
            Image buttonImage = buttonObject.GetComponent<Image>();
            buttonImage.sprite = Sprite.Create(crayonTexture, new Rect(0, 0, crayonTexture.width, crayonTexture.height), new Vector2(0, 0));
            buttonImage.color = colors[i];

            // Add a listener to the button's click event
            button.onClick.AddListener(brushButton.OnClick);
            // Update the position for the next button
            buttonIndex++;
            if (buttonIndex >= buttonsPerRow)
            {
                buttonIndex = 0;
                if (trapezoidDirection == TrapezoidDirection.Shrink)
                {
                    buttonsPerRow--;
                    rowOffset += columnXOffset / 2;
                }
                else
                {
                    buttonsPerRow++;
                    rowOffset -= columnXOffset / 2;
                }
                currentPosition = new Vector3(startPosition.x + rowOffset, currentPosition.y + yOffset, 0);
            }
            else
            {
                currentPosition += new Vector3(columnXOffset, 0, 0);
            }
            buttonObject.transform.localPosition = currentPosition;

            Debug.Log(buttonObject.transform.localPosition);
            Debug.Assert(ApproximatelyM(buttonObject.transform.localPosition.x, startPosition.x + rowOffset, (columnXOffset / 2)), $"BrushButton{i} x position is incorrect");
            //Debug.Assert(Approximately((buttonObject.transform.localPosition.x - startPosition.x - rowOffset) % (columnXOffset / 2), 0), "BrushButton x position is incorrect");
            Debug.Assert(ApproximatelyM(buttonObject.transform.localPosition.y, startPosition.y, yOffset), "BrushButton y position is incorrect");
        }
    }
    public static bool ApproximatelyM(float a, float b, float modulo, float tolerance = 0.01f)
    {
        float result = (a - b) % modulo;
        return Mathf.Abs(result) < tolerance || Mathf.Abs(result - modulo) < tolerance;
    }
}
/// <summary>
/// This is a <c cref="ColorPreset">ColorPreset</c> class.
/// </summary>
[System.Serializable]
public class ColorPreset
{
    /// <summary>
    /// The name of the preset.
    /// </summary>
    public string name;

    /// <summary>
    /// The array of colors in the preset.
    /// </summary>
    public Color[] colors;
}