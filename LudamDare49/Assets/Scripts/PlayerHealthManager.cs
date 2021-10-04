using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public FloatVariable maxHealth, currentHealth;

    public GameEvent healthChanged;
    public GameEvent playerDead;

    private void Start() {
        currentHealth.Value = maxHealth.Value;
    }

    public void Attacked(float damage)
    {
        print("PlayerAttacked");
        currentHealth.Value -= damage;
        healthChanged.Raise();
    }

    private void Update() {
        if(currentHealth.Value<0)
        {   
            playerDead.Raise(); // goes to UI to change the conditions for the menu
        }
    }

    public void gameRestart()
    {
        currentHealth.Value = maxHealth.Value;
    }
}
