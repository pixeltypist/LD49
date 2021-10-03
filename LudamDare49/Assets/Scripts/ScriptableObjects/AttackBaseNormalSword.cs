using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackBaseNormalSword", menuName = "ScriptableObjects/Attack Base Normal Sword")]
public class AttackBaseNormalSword : AttackBase
{
    public GameObject attackBoxHorizontal, attackBoxVertical;
    public GameObject hitBox;
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
