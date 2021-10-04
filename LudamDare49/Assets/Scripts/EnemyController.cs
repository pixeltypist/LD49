using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    RoomController roomController;

    public float attackDamage, attackRange, attackCooldown, speed;
    Vector2 trajectory;
    public TransformHold player;
    Transform playerLocation;
    Rigidbody2D rb;
    float distanceToPlayer;

    bool isAttacking, isFrozen, playerClose, canAttack;
    public GameObject attackTrigger;
    Animator anim;

    //BoxCollider2D hitBox;
    private void Start() 
    {
        playerLocation = player.objTransform;
        trajectory = new Vector2(0,0);
        rb = GetComponent<Rigidbody2D>();
        isFrozen = false;
        playerClose = false;
        attackTrigger.SetActive(false);
        anim = GetComponent<Animator>();
        canAttack = true;
        //hitBox = GetComponent<BoxCollider2D>();
        //hitBox.enabled = false;
    }

    private void Update() {
        trajectory = playerLocation.position - transform.position;
        distanceToPlayer = trajectory.magnitude;
        if(distanceToPlayer<1)
        {
            playerClose = true;
        }
        else{
            playerClose = false;
        }
        
        trajectory.Normalize();
    }
    private void FixedUpdate() {
        if(!isFrozen & !isAttacking & !playerClose)
        {
            rb.MovePosition(rb.position + trajectory*Time.fixedDeltaTime*speed);
        }
        if(distanceToPlayer<attackRange & canAttack)
        {
            StartAttack();
            //hitBox.enabled = true;
        }
        
    }

    /*private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthManager>().Attacked(attackDamage);
            //hitBox.enabled = false;
            Attack();
        }
    }*/

    public void StartAttack()
    {
        print("enemy start attack");
        isFrozen = true;
        canAttack = false;
        anim.SetBool("isAttacking", true);
    }
    void Attack()
    {
        print("Attacking");
        //isFrozen = true;
        //isAttacking = true;

        attackTrigger.SetActive(true);
        isAttacking = true;
        //hitBox.enabled = true;
        //do some damage
        //StartCoroutine("Cooldown");
    }

    public void SetRoomController(RoomController _roomController)
    {
        roomController = _roomController;
    }

    public void Die() 
    {
        roomController.EnemyDied(gameObject);
        Destroy(gameObject);
    }

    IEnumerator Cooldown()
    {
        print("Entered cooldown");
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        //isFrozen = false;
        //isAttacking = false;
    }

    public void FinishAttack()
    {
        print("Entered finish attack");
        isAttacking = false;
        isFrozen = false;
        attackTrigger.GetComponent<enemyPingPlayer>().hasAlreadyAttackedPlayerOnce = false;
        attackTrigger.SetActive(false);
        anim.SetBool("isAttacking", false);
        StartCoroutine("Cooldown");
    }

    public void GamePaused()
    {
        isFrozen = true;
    }

    public void GameUnPaused()
    {
        isFrozen = false;
    }
}
