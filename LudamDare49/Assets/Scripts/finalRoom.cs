using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalRoom : MonoBehaviour
{
    public GameEvent resetDungeon;

    public BoxCollider2D finalDoorCollider;

    public Animator door;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            resetDungeon.Raise();
        }
    }
}
