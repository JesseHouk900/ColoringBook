using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrayonLineRenderer : CustomLineRenderer
{

    override public void Update() 
    {
        Draw();
    }

    override public void Draw() {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Creating new brush");
            CreateBrush();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = GetComponent<GameController>().camera.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos != lastPos)
            {
                AddAPoint(mousePos);
                lastPos = mousePos;
            }
        }
        else
        {
            currentLineRenderer = null;
        }
    }

    override public void CreateBrush() 
    {
        GameObject brushInstance = Instantiate(this.brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = GetComponent<GameController>().camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    override public void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    override public void SetBrush()
    {
        
    }
}
