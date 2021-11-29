using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AOE_v2", menuName = "ScriptableObjects/v2/AOE")]
public class aoe_v2 : playerAttackBase_v2
{
    //instantiate gameObject at player position
    //add circlecollider2D to it, set radius 
    //set this object as the player's child
    //look for enemies
    //if enemy found, ping for damage

    public GameObject AOE;
    public override void CastAreaOfEffect()
    {
        Vector2 origin = new Vector2(player.objTransform.position.x, player.objTransform.position.y);
        GameObject AreaOfEffect = Instantiate(AOE, origin, Quaternion.identity);
        AreaOfEffect.transform.SetParent(player.objTransform);
        AreaOfEffect.GetComponent<CircleCollider2D>().radius = range;
        AreaOfEffect.GetComponent<aoe_controller>().PassDamage(damage);
    }
}
