using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackBaseHeavySword", menuName = "ScriptableObjects/Attack Base Heavy Sword")]
public class AttackBaseHeavySword : AttackBase
{
     public GameObject attackBoxHorizontal, attackBoxVertical, hitBox;
     public float rangeExtension;
     public override void SwordSwing(Transform activePoint, bool playerFacingY)
    {
        hitBox = null;
        if(playerFacingY)
        {
            hitBox = Instantiate(attackBoxHorizontal, activePoint.position, Quaternion.identity);
        }
        else
        {
            hitBox = Instantiate(attackBoxVertical, activePoint.position, Quaternion.identity);
        }
        hitBox.GetComponent<SwordAttack>().DealDamage(damage);
    }
    
}
