using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script that handles drawing on a canvas using a brush.
/// </summary>
public class UIComponent : MonoBehaviour
{
    public enum Layer {Cursor, Button, UIBackground, Canvas};
    
    public Layer layer;

    public void Start()
    {

        if (GetComponent<DrawingCanvas>())
        {
            Debug.Log($"transformed layer: {transform.localPosition}");

        }
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, (float)layer);
        if (GetComponent<DrawingCanvas>())
        {
            Debug.Log($"transformed layer: {transform.localPosition}");

        }
    }
}