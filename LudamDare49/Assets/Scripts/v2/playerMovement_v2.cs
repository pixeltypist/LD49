using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement_v2 : MonoBehaviour
{
    Rigidbody2D rb; 
    Animator anim;
    public float moveX, moveY, lastMovedX, lastMovedY;
    bool freeToMove = true;
    [SerializeField]float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        if(moveX == 0 & moveY == 0)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
            lastMovedX = moveX;
            lastMovedY = moveY;
            anim.SetFloat("lastMovedX", lastMovedX);
            anim.SetFloat("lastMovedY", lastMovedY);
        }
        anim.SetFloat("moveX", moveX);
        anim.SetFloat("moveY", moveY);
    }

    void FixedUpdate()
    {
        if(freeToMove)
        {
            Vector2 trajectory = new Vector2(moveX, moveY);
            trajectory.Normalize();
            rb.MovePosition(rb.position + trajectory*speed*Time.fixedDeltaTime);
        }
        
    }

    public void FreeToMove(bool p_FreeToMove)
    {
        freeToMove = p_FreeToMove;
    }
}
