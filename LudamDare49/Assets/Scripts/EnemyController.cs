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

    bool isAttacking, isFrozen;

    //BoxCollider2D hitBox;
    private void Start() 
    {
        playerLocation = player.objTransform;
        trajectory = new Vector2(0,0);
        rb = GetComponent<Rigidbody2D>();
        //hitBox = GetComponent<BoxCollider2D>();
        //hitBox.enabled = false;
    }

    private void Update() {
        trajectory = playerLocation.position - transform.position;
        distanceToPlayer = trajectory.magnitude;
        trajectory.Normalize();
    }
    private void FixedUpdate() {
        if(!isFrozen & !isAttacking)
        {
            rb.MovePosition(rb.position + trajectory*Time.fixedDeltaTime*speed);
        }
        if(distanceToPlayer<attackRange)
        {
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
    void Attack()
    {
        print("Attacking");
        isFrozen = true;
        isAttacking = true;
        //hitBox.enabled = true;
        //do some damage
        StartCoroutine("Cooldown");
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
        yield return new WaitForSeconds(attackCooldown);
        isFrozen = false;
        isAttacking = false;
    }
}
