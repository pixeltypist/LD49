using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackBaseMagicMissle", menuName = "ScriptableObjects/Attack Base Magic Missle")]
public class AttackBaseMagicMissle : AttackBase
{
    public GameObject magicMissle;
    public override void MagicMissle(Vector2 target, Transform attacker, Transform attackPoint)
    {
        Vector2 trajectory = new Vector2(target.x - attacker.position.x, target.y - attacker.position.y);
        trajectory.Normalize();
        GameObject missle = Instantiate(magicMissle, attackPoint.position, Quaternion.identity);
        missle.GetComponent<MagicMissle>().SetTrajectory(trajectory);
    }
}
