using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]FloatVariable speed, heavySwordDelay, AOEdelay;
    float moveX,moveY,chargeStart,chargeEnd, currentCharge;
    Vector2 trajectory;
    bool stopMoving, isAttacking, chargingAttack, chargeReleased, cooldownActive, waitForRelease;
    //Animator anim;
    [SerializeField]AttackBase normalSword, heavySword, magicMissiles, magicAOE;
    [SerializeField]AttackContainer attackContainer;
    [SerializeField]Camera cam;

    public Transform attackPoint_u, attackPoint_r, attackPoint_d, attackPoint_l, activePoint;
    Animator anim;
    public bool isFacingY;
    AttackBase activeAttackBase;
    public string boolToChange;
    GameObject hitBox;
    public CooldownUI cooldownUI; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chargingAttack = false;
        stopMoving = false;
        cooldownActive = false;
        anim = GetComponent<Animator>();
        chargeReleased = true;
        hitBox = null;

        attackContainer.RandomWeapon();
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
        

        //attack logic
        if(Input.GetMouseButtonUp(0))
        {
            if(chargingAttack & !waitForRelease)
            {
                anim.SetBool("isAttacking", false);
            }
            print("Let go of Mouse button");
            chargingAttack = false;
            chargeReleased = true;
            chargeStart = 0;
            currentCharge = 0;
            chargeEnd = 0;
            anim.SetBool(boolToChange, false);
            if(waitForRelease)
            {
                anim.SetBool("releaseHold", true);
                if(activeAttackBase == heavySword)
                {
                    //anim.SetBool("usingHeavySword", true);
                    hitBox = Instantiate(heavySword.SwordSwing(isFacingY), activePoint.position, Quaternion.identity);
                    hitBox.transform.parent = transform;
                    hitBox.GetComponent<SwordAttack>().UpdateDamage(heavySword.damage);
                    waitForRelease = false;
                    //anim.SetBool("releaseHold", false);
                }
                else if(activeAttackBase == magicAOE)
                {
                    //anim.SetBool("usingHeavySword", true);
                    hitBox = Instantiate(magicAOE.AOE(), transform.position, Quaternion.identity);
                    hitBox.transform.parent = transform;
                    
                    //hitBox.GetComponent<SwordAttack>().UpdateDamage(heavySword.damage);
                    waitForRelease = false;
                    //anim.SetBool("releaseHold", false);
                }

                
            }
        }
        
        if(chargeReleased)
        {
            if((Input.GetMouseButtonDown(0) & !isAttacking) | chargingAttack)
            {
                print("pressed mousebutton");
                Attack();
            }
        }

        if(waitForRelease)
        {
            anim.SetBool("holdingCharge", true);
        }
        else
        {
            anim.SetBool("holdingCharge", false);
        }

    }
    
    private void FixedUpdate() {
        if(!stopMoving & !chargingAttack)
        {
            //movement
            anim.SetFloat("lastMovedX", moveX);
            anim.SetFloat("lastMovedY", moveY);
            trajectory = new Vector2(moveX, moveY);
            trajectory.Normalize();
            rb.MovePosition(rb.position + trajectory*speed.Value*Time.fixedDeltaTime);
        }
        
    }

    void Attack()
    {
        if(!cooldownActive)
        {
            
            anim.SetBool("isAttacking", true);
            AttackBase activeBase = attackContainer.DetermineActiveAttack();
            activeAttackBase = activeBase;
            DetermineActiveAttackBox();
            if(activeBase == normalSword)
            {
                stopMoving = true;
                print("UsingNormalSword");
                anim.SetBool("usingNormalSword", true);
                boolToChange = "usingNormalSword";
                hitBox = Instantiate(normalSword.SwordSwing(isFacingY), activePoint.position, Quaternion.identity);
                hitBox.transform.parent = transform;
                hitBox.GetComponent<SwordAttack>().UpdateDamage(normalSword.damage);
                //anim.SetBool("usingNormalSwordTest", false);
                //print("SwordSwing");
            }
            else if(activeBase == heavySword)
            {
                /*if(!attackHold)
                {
                    print("Active weapon: heavy sword");
                    chargeStart = Time.fixedTime;
                    chargeEnd = chargeStart;
                    attackHold = !attackHold;
                }
                chargeEnd += Time.fixedDeltaTime;
                print("charging");
                if(chargeEnd-chargeStart >= heavySwordDelay.Value )
                {
                    //heavySword.SwordSwing(activePoint, isFacingY);
                    attackHold = false;
                    isAttacking = true;
                    chargeReleased = false;
                    print("Heavy Sword swing");
                }*/

                //if not currently charging, start charging
                anim.SetBool("usingHeavySword", true);
                boolToChange = "usingHeavySword";
                if(!chargingAttack)
                {
                    chargingAttack = true;
                    chargeStart = Time.time;
                    currentCharge = chargeStart;
                    chargeEnd = chargeStart + heavySwordDelay.Value;
                }
                //increment a timer(if charging)
                if(chargingAttack)
                {
                    currentCharge += Time.deltaTime;
                }
                //once timer is up, when you let go of mousebutton, you'll release the charge?
                if(currentCharge >= chargeEnd)
                {
                    print("Ready to release charge");
                    waitForRelease = true;
                }
            }
            else if(activeBase == magicMissiles)
            {
                //anim.SetBool("usingMagicMissiles", true);
                //boolToChange = "usingMagicMissiles";

                DetermineActiveAttackBox();
                anim.SetBool("usingMagicMissiles", true);
                boolToChange = "usingMagicMissiles";
                Vector2 target = cam.ScreenToWorldPoint(Input.mousePosition);
                magicMissiles.MagicMissle(target, transform, activePoint);
                StartCoroutine(Cooldown(magicMissiles.cooldown));
            }
            else if(activeBase == magicAOE)
            {
                //magicAOE.AOE(GetComponent<TransformSetter>().transformHold, this.gameObject);

                anim.SetBool("usingAOE", true);
                boolToChange = "usingAOE";
                if(!chargingAttack)
                {
                    chargingAttack = true;
                    chargeStart = Time.time;
                    currentCharge = chargeStart;
                    chargeEnd = chargeStart + AOEdelay.Value;
                }
                //increment a timer(if charging)
                if(chargingAttack)
                {
                    currentCharge += Time.deltaTime;
                }
                //once timer is up, when you let go of mousebutton, you'll release the charge?
                if(currentCharge >= chargeEnd)
                {
                    print("Ready to release charge");
                    waitForRelease = true;
                }
            }
            else{
                print("No active Attack base!");
            }

            
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
            //other.gameObject.GetComponent<EnemyController>().Die();
        }
    }

    public void FinishAttack()
    {
        stopMoving = false;
        anim.SetBool("isAttacking", false);
        anim.SetBool(boolToChange, false);
        anim.SetBool("releaseHold", false);
        if(hitBox != null)
        {
            Destroy(hitBox);
        }
        StartCoroutine(Cooldown(activeAttackBase.cooldown));
    }
    IEnumerator Cooldown(float cooldownTime)
    {
        /*print("CoolingDown");
        yield return new WaitForSeconds(cooldownTime);
        print("Done cooldown");*/
        float startTime = Time.time;
        float currentTime = startTime;
        float endTime = startTime+cooldownTime;
        cooldownActive = true;
        while(currentTime<endTime)
        {
            currentTime+=Time.deltaTime;
            cooldownUI.AdjustSlider(cooldownTime, endTime-currentTime);
            yield return null;
        }
        cooldownActive = false;
    }

    public void GamePaused()
    {
        stopMoving = true;
    }

    public void GameUnPaused()
    {
        stopMoving = false;
    }

}
