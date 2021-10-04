using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPingPlayer : MonoBehaviour
{
    CircleCollider2D playerTrigger;
    public FloatVariable damage;
    public bool hasAlreadyAttackedPlayerOnce;
    //public bool pingedPlayer;
    void Start()
    {
        playerTrigger = GetComponent<CircleCollider2D>();

        //pingedPlayer = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") & !hasAlreadyAttackedPlayerOnce)
        {
            print("pinging player");
            other.gameObject.GetComponent<PlayerHealthManager>().Attacked(damage.Value);
            //hasAlreadyAttackedPlayerOnce = true;
        }
    }
}
