// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EventManager : MonoBehaviour 
// {
//     public delegate void OnClickAction();
//     public static event OnClickAction OnClicked;
//     public delegate void OnHeldAction();
//     public static event OnHeldAction OnHeld;
//     public delegate void OnReleasedAction();
//     public static event OnReleasedAction OnReleased;

//     public void OnGUI()
//     {
//         Event e = Event.current;
//         Debug.Log(e);
//         if (GUI.Button(new Rect(0, Screen.height, Screen.width, Screen.height * 3 / 5), "Click"))
//         {
//             Debug.Log("Click");
//             if (Input.GetMouseButtonDown(0))
//             {   
//                 if (OnClicked != null)
//                 {
//                     OnClicked();
//                 }
//             }
//             // held
//             else if (Input.GetMouseButton(0)) 
//             {
//                 if (OnHeld != null)
//                 {
//                     OnHeld();
//                 }
//             }
//             if (Input.GetMouseButtonUp(0))
//             {
//                 if (OnReleased != null)
//                 {
//                     OnReleased();
//                 }
//             }
//             // for (int i = 0; i < GetComponent<BrushStrokeManager>().brushStrokes.Count; i++)
//             // {
//             //     if (GetComponent<BrushStrokeManager>().brushStrokes[i].GetComponent<BrushStroke>().isActive)
//             //     {
//             //         Graphics.DrawMeshNow(GetComponent<BrushStrokeManager>().brushStrokes[i].GetComponent<BrushStroke>().GetMesh(), Vector3.zero, Quaternion.identity, 0);
//             //     }
//             //}

//         }
//     }



// }