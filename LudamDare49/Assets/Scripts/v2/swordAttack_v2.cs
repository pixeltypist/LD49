using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Sword Attack", menuName ="ScriptableObjects/v2/Sword Attack")]
public class swordAttack_v2 : playerAttackBase_v2
{
    public override void SwordSwing(Animator anim)
    {
        anim.SetBool("usingNormalSword", true);
        if(FindEnemy() != null)
        {
            FindEnemy().GetComponent<EnemyHealthManager>().Attacked(damage);
        }
    }
}
