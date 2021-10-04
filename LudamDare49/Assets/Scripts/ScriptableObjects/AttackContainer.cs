using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "AttackContainer", menuName = "ScriptableObjects/AttackContainer")]
public class AttackContainer : ScriptableObject 
{
    public List<AttackBase> attackBases;
    public AttackBase activeBase;

    public GameEvent updateWeaponUI;

    public AttackBase DetermineActiveAttack()
    {
        for(int i = 0; i<attackBases.Count; i++)
        {
            if(attackBases[i].isActive == true)
            {
                activeBase = attackBases[i];
            }
        }
        return activeBase;
    }

    public void RandomWeapon()
    {
        int randomIndex = Random.Range(0,attackBases.Count);
        if(attackBases[randomIndex] != activeBase)
        {
            activeBase.ChangeOut();
            activeBase = attackBases[randomIndex];
            activeBase.ChangeOut();
        }
        if(attackBases[randomIndex] == activeBase)
        {
            activeBase.RaiseEvent();
        }

        //updateWeaponUI.Raise();

    }
}
