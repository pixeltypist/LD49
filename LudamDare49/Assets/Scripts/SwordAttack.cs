using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    BoxCollider2D hitBox;
    List<GameObject> enemiesHit;
    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            enemiesHit.Add(other.gameObject);
        }    
    }

    public void DealDamage(float damage)
    {
        foreach(var enemy in enemiesHit)
        {
            enemy.GetComponent<EnemyHealthManager>().Attacked(damage);
        }
        Destroy(this.gameObject);
    }
}
