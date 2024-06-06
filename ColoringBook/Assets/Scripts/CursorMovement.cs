using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public Vector3 offset;
    private Vector3 mousePosition;
    //public float Speed;
    //public Texture2D myCursorTexture;
    //public CursorMode myCursorMode = CursorMode.Auto;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    //private Vector2 hotSpot = Vector2.zero;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.visible = false;
        myRigidbody = GetComponent<Rigidbody2D>();
        //myCursorTexture = GetComponent<SpriteRenderer>().sprite.texture;
        //Cursor.SetCursor(myCursorTexture, hotSpot, myCursorMode);


    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal"); //Input.GetAxis
        change.y = Input.GetAxisRaw("Vertical");
        //if (change != Vector3.zero)
        //{
        //    MoveCharacter();
        //}
        //var parentObject = GetComponentInParent<Transform>().tag;


        //Debug.Log(change);


    }

    void LateUpdate()
    {
        if (1 == 1)
        {
            MoveCharacter();
            PlayAnimations();
        }
    }


    void MoveCharacter()
    {
        Vector3 newPosition = CalculateNewPosition();
        UpdatePosition(newPosition);

        GameObject.Find("DrawingCanvas").GetComponent<DrawingCanvas>().Refresh();
    }

    public Vector3 CalculateNewPosition()
    {

        //myRigidbody.MovePosition(
        //    transform.position + change * Speed * Time.deltaTime
        //    );
        Vector2 com = myRigidbody.centerOfMass;
        offset = new Vector3(-com.x, -com.y, -5);
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition() //Camera.main.ScreenToWorldPoint(Input.mousePosition)
            + offset;
        //Debug.Log("Cursor: " + mousePosition);
        Vector3 smoothedPosition =
        Vector2.Lerp(transform.position, mousePosition,
        moveSpeed);
        //pos.x = Mathf.Clamp(pos.x, 0, 9);
        //pos.y = Mathf.Clamp(pos.y, 0, 19);
        return smoothedPosition;
    }

    public void UpdatePosition(Vector3 pos)
    {
        transform.position = pos;
        //transform.position += change * Speed * Time.deltaTime;
        //var pos = Camera.main.WorldToViewportPoint(transform.position);
        //transform.position = Camera.main.ViewportToWorldPoint(pos);
        //Debug.Log(transform.position);
        myRigidbody.MovePosition(transform.position);

    }

    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;
    void PlayAnimations()
    {
        //Get them_Animator, which you attach to the GameObject you intend to animate.
        //this.animator = gameObject.GetComponent<Animator>();
        //Fetch the current Animation clip information for the base layer
        m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        // //Access the current length of the clip
        // m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        //Access the Animation clip name
        m_ClipName = m_CurrentClipInfo[0].clip.name;
        //Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        animator.SetFloat("x", change.x);
        animator.SetFloat("y", change.y);
    }
}
