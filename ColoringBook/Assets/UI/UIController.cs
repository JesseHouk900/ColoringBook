using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public MouseEventHandler mouseHandler;
    // Start is called before the first frame update
    void Start()
    {
        mouseHandler = new MouseEventHandler();
        
        // entry.callback.AddListener((data) => { mouseHandler.OnClick((PointerEventData)data); });
        // UpdateEventTrigger(EventTriggerType.PointerDown, (data) => { mouseHandler.OnClick(); });
        // UpdateEventTrigger(EventTriggerType.PointerUp, (data) => { mouseHandler.OnRelease(); });
        
        // entry.eventID = EventTriggerType.PointerUp;
        // entry.callback.AddListener((data) => { mouseHandler.OnRelease(); });
        // eventTrigger.triggers.Add(entry);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // public void UpdateEventTrigger(EventTriggerType triggerType, Event callback)
    // {
        
    //     var eventTrigger = GetComponent<EventTrigger>(); 
        
    //     EventTrigger.Entry entry = new EventTrigger.Entry();
    //     entry.eventID = triggerType;
    //     entry.callback.AddListener(callback);
    //     eventTrigger.triggers.Add(entry);
    // }

    void BrushSampleSelected() 
    {
        
    }
}


