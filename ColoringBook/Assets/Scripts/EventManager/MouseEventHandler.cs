using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A script that handles mouse events and triggers corresponding delegates.
/// </summary>
public class MouseEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler//, IDragHandler
{
    /// <summary>
    /// A delegate that represents a method to be called when a mouse event occurs.
    /// </summary>
    public delegate void MouseEventHandlerDelegate();

    /// <summary>
    /// A delegate that is called when the mouse is clicked.
    /// </summary>
    public MouseEventHandlerDelegate OnClick;

    /// <summary>
    /// A delegate that is called when the mouse button is released.
    /// </summary>
    public MouseEventHandlerDelegate OnRelease;

    /// <summary>
    /// A delegate that is called when the mouse button is held.
    /// </summary>
    public MouseEventHandlerDelegate OnHold;

    /// <summary>
    /// A delegate that is called when the mouse is dragged.
    /// </summary>
    public MouseEventHandlerDelegate OnDrag;

    /// <summary>
    /// A flag indicating whether the mouse button is being held.
    /// </summary>
    private bool isHolding = false;

    /// <summary>
    /// Initializes the event triggers for mouse events.
    /// </summary>
    private void Start()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();

        // Initial Button Press
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry);

        // Release Mouse Button
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        eventTrigger.triggers.Add(entry);

        // Press and release trigger
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        eventTrigger.triggers.Add(entry);

        // Move Mouse
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnPointerDrag((PointerEventData)data); });
        eventTrigger.triggers.Add(entry);
    }

    /// <summary>
    /// Called when the mouse button is pressed.
    /// </summary>
    /// <param name="eventData">The event data for the mouse press.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 ray = eventData.position;
        Collider2D[] colliders = Physics2D.OverlapPointAll(ray);
        // Check if there is a bursh being selected
        BrushButton selectedBrushButton = GetTopBrushButton(colliders);
        if (selectedBrushButton != null)
        {
            UpdateGameControllerBrush(selectedBrushButton);
        }
        // Include additional checks here such as blocking boarder drawing 
        // or drawing over the undo button

        if (OnClick != null)
        {
            OnClick();
        }
        isHolding = true;
    }

    private BrushButton GetTopBrushButton(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider != null)
            {
                BrushButton[] brushButtons = collider.gameObject.GetComponents<BrushButton>();
                if (brushButtons.Length > 0)
                {
                    return brushButtons[0];
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Update the `gameController.brush` with `topBrushButton.brush` and add a new stroke  
    /// </summary>
    /// <param name="topBrushButton"></param>
    private void UpdateGameControllerBrush(BrushButton topBrushButton)
    {
        GameObject gameControllerObject = GameObject.Find("GameController");
        if (gameControllerObject != null)
        {
            GameController gameController = gameControllerObject.GetComponent<GameController>();
            if (gameController != null)
            {
                gameController.brush = topBrushButton.brush;
                Debug.Log(topBrushButton);
                // Create a new brush stroke with the selected brush
                BrushStroke brushStroke = new BrushStroke(topBrushButton.brush, new List<Vector2>());
                gameController.brushStrokes.Add(brushStroke);
            }
        }
    }

    /// <summary>
    /// Called when the mouse button is released.
    /// </summary>
    /// <param name="eventData">The event data for the mouse release.</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnRelease != null)
            OnRelease();
        isHolding = false;
    }

    /// <summary>
    /// Called when the mouse is clicked.
    /// </summary>
    /// <param name="eventData">The event data for the mouse click.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClick != null)
            OnClick();
        isHolding = isHolding;
    }
    /// <summary>
    /// Called when the mouse is dragged.
    /// </summary>
    /// <param name="eventData">The event data for the mouse drag.</param>
    public void OnPointerDrag(PointerEventData eventData)
    {
        if (OnDrag != null)
            OnDrag();
    }

    /// <summary>
    /// Updates the state of the mouse button hold.
    /// </summary>
    private void Update()
    {
        if (isHolding && OnHold != null)
            OnHold();
    }
}