using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    BoxCollider2D hitBox;
    public List<GameObject> enemiesHit;
    float damageReceived;
    bool enemyHit;
    void Start()
    {
        print("Instantiated hitbox");
        hitBox = GetComponent<BoxCollider2D>();
        enemyHit = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            enemiesHit.Add(other.gameObject);
        }    
    }

    public void UpdateDamage(float _damage)
    {
        damageReceived = _damage;
    }
    private void Update() {
        DealDamage(damageReceived);
    }
    public void DealDamage(float damage)
    {
        print("Trying to deal Damage");
        if(enemiesHit.Count > 0)
        {
            print("Entered if statement");
            for(int i = 0; i<enemiesHit.Count; i++)
            {
                print("Pinging enemy health manager");
                enemiesHit[i].GetComponent<EnemyHealthManager>().Attacked(damage);
            }
            enemyHit = true;
            /*foreach(var enemy in enemiesHit)
            {
                print("Pinging enemy health manager");
                enemy.GetComponent<EnemyHealthManager>().Attacked(damage);
            }*/
        }
        if(enemyHit)
        {
            Destroy(this.gameObject);
        }
    }
}
