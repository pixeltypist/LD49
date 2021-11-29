using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement_v2_2 : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    float moveX, moveY, lastMovedX, lastMovedY;

    string currentState, qState;
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
    }

    void FixedUpdate()
    {
        Vector2 trajectory = new Vector2(moveX, moveY);
        trajectory.Normalize();
        rb.MovePosition(rb.position + trajectory*speed*Time.fixedDeltaTime);
    }

    void UpdateAnimator()
    {
        if(moveX != 0)
        {
            if (moveX == 1) qState = "player_walk_right";
            else qState = "player_walk_left";
        }
        else if(moveY != 0)
        {
            if (moveY == 1) qState = "player_walk_up";
            else qState = "player_walk_down";
        }

        ChangeAnimationState(qState);
    }

    void ChangeAnimationState(string newState) {
        {
            if (currentState == newState) return;

            anim.Play(newState);

            currentState = newState;
        }
    }
}
