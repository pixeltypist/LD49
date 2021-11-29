using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "missile_v2", menuName = "ScriptableObjects/v2/Magic Missile")]
public class missile_v2 : playerAttackBase_v2
{
    public GameObject missile; // this is the thing the script is going to instantiate
    public override void ShootMissile()
    {
        Vector2 origin = new Vector2(player.objTransform.position.x, player.objTransform.position.y);
        Vector2 direction = DetermineDirection();
        if(direction.x != 0)
        {
            origin.y-=0.25f;
        }
        else
        {
            origin.x+=0.25f;
        }
        GameObject misToFire = Instantiate(missile, origin, Quaternion.identity);
        misToFire.GetComponent<missileController>().SetTrajectory(direction, enemyLayer, damage, range);
    }

    //Instantiate projectile

    //Pass trajectory to projectile for use.
}
