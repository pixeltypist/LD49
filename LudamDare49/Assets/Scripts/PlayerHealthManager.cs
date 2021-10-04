using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public FloatVariable maxHealth, currentHealth;

    public GameEvent healthChanged;

    private void Start() {
        currentHealth.Value = maxHealth.Value;
    }

    public void Attacked(float damage)
    {
        print("PlayerAttacked");
        currentHealth.Value -= damage;
        healthChanged.Raise();
    }
}
