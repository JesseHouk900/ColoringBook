using UnityEngine;
using UnityEngine.EventSystems;

// New Script: BaseMovementScript
public class BaseMovementScript : MonoBehaviour
{
    public Vector3 targetPosition;

    private void Awake()
    {
    }

    // You can customize the movement behavior in subclasses
    protected virtual void Update()
    {
        // Smoothly move the object's position towards the targetPosition
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
    }

    protected virtual void UpdateTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
    }
}