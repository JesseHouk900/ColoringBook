using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// namespace FuntionallySuperfluous
// {
public class Utils 
{

    public static Vector3 GetMouseWorldPosition() 
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    // Smooth mouse position calculation using Lerp
    public static Vector3 CalculateSmoothedPosition(Vector3 currentPosition, Vector3 targetPosition, float smoothFactor)
    {
        return Vector3.Lerp(currentPosition, targetPosition, smoothFactor);
    }
}
    
    
//}