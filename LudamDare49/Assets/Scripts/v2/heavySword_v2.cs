using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="heavySwordv2", menuName = "ScriptableObjects/v2/Heavy Sword")]
public class heavySword_v2 : playerAttackBase_v2
{
    public override void HeavySword()
    {
        
        if(FindEnemy() != null)
        {
            FindEnemy().GetComponent<EnemyHealthManager>().Attacked(damage);
        }
    }
}
