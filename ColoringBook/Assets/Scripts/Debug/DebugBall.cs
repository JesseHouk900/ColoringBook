using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DebugBall : MonoBehaviour
{
    public Vector3 position;

    public void OnValidate()
    {
        SpriteRenderer sprite_r = GetComponent<SpriteRenderer>();
        Debug.Log($"Render Position: {sprite_r.transform.position}");
        Debug.Log($"Inspector Position: {transform.position}");
        sprite_r.transform.position = transform.position;
    }
}
