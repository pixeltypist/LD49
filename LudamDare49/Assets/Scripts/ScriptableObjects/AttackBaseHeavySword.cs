using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackBaseHeavySword", menuName = "ScriptableObjects/Attack Base Heavy Sword")]
public class AttackBaseHeavySword : AttackBase
{
     public GameObject attackBoxHorizontal, attackBoxVertical;
     //public float rangeExtension;
     public override GameObject SwordSwing(bool playerFacingY)
    {
        if(playerFacingY)
        {
            return attackBoxHorizontal; 
            //Instantiate(attackBoxHorizontal, activePoint.position, Quaternion.identity);
        }
        else
        {
            return attackBoxVertical;
            //hitBox = Instantiate(attackBoxVertical, activePoint.position, Quaternion.identity);
        }

        //hitBox.GetComponent<SwordAttack>().DealDamage(damage);
    }
    
}
