using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackBaseMagicAOE", menuName = "ScriptableObjects/Attack Base Magic AOE")]
public class AttackBaseMagicAOE : AttackBase
{
   public GameObject aoe;

   public override GameObject AOE()
   {
       return aoe;
       //Vector3 playerPosition = new Vector3(playerPos.objTransform.position.x, playerPos.objTransform.position.y, 0);
       //GameObject areaOfEffect = Instantiate(aoe, playerPosition, Quaternion.identity);
       //areaOfEffect.transform.parent = playerObj.transform;
       //return areaOfEffect;
   }
}
