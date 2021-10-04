using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    Animator anim;
    [SerializeField]TransformHold transformHold;
    public FloatVariable AOEdamage;
    BoxCollider2D hitBox;
    public List<GameObject> enemiesHit;
    bool enemyHit;
    void Start()
    {
        anim = GetComponent<Animator>();
        print("Instantiated hitbox");
        hitBox = GetComponent<BoxCollider2D>();
        enemyHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        DealDamage(AOEdamage.Value);
    
    }

    /*private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            GetComponent<EnemyHealthManager>().Attacked(AOEdamage.Value);
        }
        Destroy(this.gameObject);    
    }*/

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            enemiesHit.Add(other.gameObject);
        }    
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
        /*if(enemyHit)
        {
            Destroy(this.gameObject);
        }*/
    }

    public void DestroyAOE()
    {
        Destroy(this.gameObject);
    }
}
