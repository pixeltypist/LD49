using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]FloatVariable speed, heavySwordDelay;
    float moveX,moveY,chargeStart,chargeEnd;
    Vector2 trajectory;
    bool stopMoving, isAttacking, attackHold, resetHold;
    //Animator anim;
    [SerializeField]AttackBase normalSword, heavySword, magicMissiles, magicAOE;
    [SerializeField]AttackContainer attackContainer;
    [SerializeField]Camera cam;

    public Transform attackPoint_u, attackPoint_r, attackPoint_d, attackPoint_l, activePoint;
    Animator anim;
    public bool isFacingY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackHold = false;
        stopMoving = false;
        anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        if(!stopMoving)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
        }
        anim.SetFloat("moveX", moveX);
        anim.SetFloat("moveY", moveY);
        anim.SetFloat("lastMovedX", moveX);
        anim.SetFloat("lastMovedY", moveY);

    //attack logic
    if(Input.GetMouseButtonUp(0))
        {
            print("Let go of Mouse button");
            attackHold = false;
            resetHold = true;
            chargeStart =0;
            chargeEnd = 0;
        }
    if(!resetHold)
    {
        if((Input.GetMouseButtonDown(0) & !isAttacking) | attackHold)
        {
            print("pressed mousebutton");
            Attack();
        }
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
        stopMoving = true;
        anim.SetBool("isAttacking", true);
        AttackBase activeBase = attackContainer.DetermineActiveAttack();
        DetermineActiveAttackBox();
        if(activeBase == normalSword){
            normalSword.SwordSwing(activePoint, isFacingY);
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
            if(chargeEnd-chargeStart >= heavySwordDelay.Value ){
                heavySword.SwordSwing(activePoint, isFacingY);
                attackHold = false;
                resetHold = true;
                print("Heavy Sword swing");
            }
            
        }
        else if(activeBase == magicMissiles)
        {
            Vector2 target = cam.ScreenToWorldPoint(Input.mousePosition);
            magicMissiles.MagicMissle(target, transform, activePoint);
        }
        else if(activeBase == magicAOE)
        {
            magicAOE.AOE(GetComponent<TransformSetter>().transformHold, this.gameObject);
        }
        else{
            print("No active Attack base!");
        }
    }

    void DetermineActiveAttackBox()
    {
        if(moveX != 0)
        {
            if (moveX == 1)
            {
                activePoint = attackPoint_r;
            }
            else 
            {
                activePoint = attackPoint_l;
            }
            isFacingY = false;
        }

        else if(moveY != 0)
        {
            if(moveY == 1)
            {
                activePoint = attackPoint_u;
            }
            else
            {
                activePoint = attackPoint_d;
            }
            isFacingY = true;
        }
        else
        {
            activePoint = attackPoint_d;
            isFacingY = true;
        }
    }

    public void RandomizeWeapon()
    {
        attackContainer.RandomWeapon();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().Die();
        }
    }

    public void FinishAttack()
    {
        stopMoving = false;
        anim.SetBool("isAttacking", false);
    }
}
