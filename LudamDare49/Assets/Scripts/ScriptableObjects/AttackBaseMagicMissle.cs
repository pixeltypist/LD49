using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "AttackBaseMagicMissle", menuName = "ScriptableObjects/Attack Base Magic Missle")]
public class AttackBaseMagicMissle : AttackBase
{
    public GameObject magicMissle;
    public override void MagicMissle(Vector2 target, Transform attacker, Transform attackPoint)
    {
        Vector2 trajectory = new Vector2(target.x - attacker.position.x, target.y - attacker.position.y);
        string boolToSet = CheckForBoolToSet(trajectory);

        trajectory.Normalize();
        GameObject missle = Instantiate(magicMissle, attackPoint.position, Quaternion.identity);
        missle.GetComponent<MagicMissle>().SetTrajectory(trajectory, boolToSet);
    }

    string CheckForBoolToSet(Vector2 trajectory)
    {
        if(trajectory.x > 0)
        {
            if(trajectory.y > 0)
            {
                if(trajectory.x>trajectory.y)
                {
                    return "right";
                }
                else
                {
                    return "up";
                }
            }
            else
            {
                float y = Math.Abs(trajectory.y);
                if(trajectory.x > y)
                {
                    return "right";
                }
                else
                {
                    return "down";
                }
            }
        }
        else
        {
            float x = Math.Abs(trajectory.x);
            if(trajectory.y > 0)
            {
                if(x>trajectory.y)
                {
                    return "left";
                }
                else
                {
                    return "up";
                }
            }
            else
            {
                float y = Math.Abs(trajectory.y);
                if(x>y){
                    return "left";
                }
                else
                {
                    return "down";
                }
            }
        }
    }
}
