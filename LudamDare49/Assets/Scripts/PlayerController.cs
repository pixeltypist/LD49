using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]FloatVariable speed, cooldown, heavySwordDelay;
    float moveX,moveY,chargeStart,chargeEnd;
    Vector2 trajectory;
    bool stopMoving, isAttacking, attackHold;
    //Animator anim;
    [SerializeField]AttackBase normalSword, heavySword, magicMissiles, magicAOE;
    [SerializeField]AttackContainer attackContainer;
    [SerializeField]Camera cam;

    public Transform attackPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackHold = false;
        stopMoving = false;
    }

    private void Update() 
    {
       moveX = Input.GetAxisRaw("Horizontal");
       moveY = Input.GetAxisRaw("Vertical");
        
    //attack logic
    if(Input.GetMouseButtonUp(0))
        {
            print("Let go of Mouse button");
            attackHold = false;
            chargeStart =0;
            chargeEnd = 0;
        }
    if(Input.GetMouseButtonDown(0) | attackHold & !isAttacking)
        {
            print("pressed mousebutton");
            Attack();
        }
    }

    private void FixedUpdate() {
        if(!stopMoving)
        {
            //movement
            trajectory = new Vector2(moveX, moveY);
            trajectory.Normalize();
            rb.MovePosition(rb.position + trajectory*speed.Value*Time.fixedDeltaTime);
        }
        
    }

    void Attack()
    {
        AttackBase activeBase = attackContainer.DetermineActiveAttack();
        if(activeBase == normalSword){
            normalSword.SwordSwing();
            print("SwordSwing");
        }
        else if(activeBase == heavySword){
            if(!attackHold)
            {
                print("Active weapon: heavy sword");
                chargeStart = Time.fixedTime;
                chargeEnd = chargeStart;
                attackHold = !attackHold;
            }
            chargeEnd += Time.fixedDeltaTime;
            if(chargeEnd-chargeStart >= heavySwordDelay.Value){
                heavySword.SwordSwing();
                print("Heavy Sword swing");
            }
            
        }
        else if(activeBase == magicMissiles)
        {
            Vector2 target = cam.ScreenToWorldPoint(Input.mousePosition);
            magicMissiles.MagicMissle(target, transform, attackPoint);
        }
        else if(activeBase == magicAOE)
        {
            magicAOE.AOE(GetComponent<TransformSetter>().transformHold, this.gameObject);
        }
        else{
            print("No active Attack base!");
        }
    }

    public void RandomizeWeapon()
    {
        attackContainer.RandomWeapon();
    }
}
