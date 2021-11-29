using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    private void Start() 
    {
        currentHealth = maxHealth;    
    }

    public void Attacked(float damage)
    {
        currentHealth -= damage;
        /*if(currentHealth <= 0)
        {
            GetComponent<EnemyController>().Die();
        }*/

        Destroy(this.gameObject);
    }
}
