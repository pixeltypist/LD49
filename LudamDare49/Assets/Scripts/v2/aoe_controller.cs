using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoe_controller : MonoBehaviour
{
    CircleCollider2D col;
    float damage;
    //Transform player;
    Animator anim;
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    public void PassDamage(float pDamage)
    {
        damage = pDamage;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealthManager>().Attacked(damage);
        }
    }

    void Disintegrate()
    {
        Destroy(this.gameObject);
    }
}
