using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat_v2 : MonoBehaviour
{
    public playerAttackBase_v2 SwordAttack, HammerAttack, MissileAttack, BurstAttack, activeAttack;
    public List<playerAttackBase_v2> attacks;
    string boolToChange; // this is to make the FinishAttack() reusable
    bool charging, doneCharging;
    float timeDoneCharging, currentTime;
    public GameEvent changedWeapon;
    //public List<playerAttackBase_v2> availableAttacks = new List<playerAttackBase_v2>();

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            SetBoolToChange();

            if (activeAttack.chargeTime != 0 && !doneCharging) 
                ChargeAttack();
            else 
                Attack();
        }

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
        {
            charging = false;
            currentTime = 0;
            if(doneCharging)
                Attack();
            else
            {
                FinishAttack();
            }
            doneCharging = false;
        }
            
        
        if (charging && !doneCharging)
            ChargeAttack();

        GetComponent<playerMovement_v2>().FreeToMove(!(GetComponent<Animator>().GetBool("isAttacking")));
        
}

    void Attack()
    {
        // this will set the plyaer down one of the "attacking" networks
        GetComponent<Animator>().SetBool("isAttacking", true);
        if(activeAttack == SwordAttack)
        {
            //boolToChange = "usingNormalSword";
            SwordAttack.SwordSwing(GetComponent<Animator>());
        }

        else if(activeAttack == HammerAttack)
        {
            GetComponent<Animator>().SetBool("releaseHold", true);
            HammerAttack.HeavySword();
        }
        else if(activeAttack == MissileAttack)
        {
            //turned off for now because it looks janky af >.>
            GetComponent<Animator>().SetBool(boolToChange, true);
            MissileAttack.ShootMissile();
        }
        else if(activeAttack == BurstAttack)
        {
            GetComponent<Animator>().SetBool("releaseHold", true);
            BurstAttack.CastAreaOfEffect();
        }
    }

    void ChargeAttack()
    {
        GetComponent<Animator>().SetBool(boolToChange, true);
        GetComponent<Animator>().SetBool("isAttacking", true);
        if (!charging)
        {
            charging = true;
            currentTime = Time.time;
            timeDoneCharging = Time.time + activeAttack.chargeTime;
        }
            

        currentTime += Time.deltaTime;
        if(currentTime >= timeDoneCharging)
        {
            doneCharging = true;
            GetComponent<Animator>().SetBool("holdingCharge", true);
        }
    }

    public void FinishAttack()
    {
        GetComponent<Animator>().SetBool(boolToChange, false);
        GetComponent<Animator>().SetBool("isAttacking", false);
        GetComponent<Animator>().SetBool("releaseHold", false);
        GetComponent<Animator>().SetBool("holdingCharge", false);
    }

    /*IEnumerator ChargeAttack(float chargeTime)
    {
        charging = true;
        yield return new WaitForSeconds(chargeTime);
        doneCharging = true;
        GetComponent<Animator>().SetBool("holdingCharge", true);
    }*/

    void SetBoolToChange()
    {
        if(activeAttack == SwordAttack)
            boolToChange = "usingNormalSword";
        else if (activeAttack == HammerAttack)
            boolToChange = "usingHeavySword";
        else if (activeAttack == MissileAttack)
            boolToChange = "usingMagicMissiles";
        else if(activeAttack == BurstAttack)
            boolToChange = "usingAOE";
    }

    public void RandomizeWeapon()
    {
        List<playerAttackBase_v2> availableAttacks = new List<playerAttackBase_v2>();
        foreach(var attack in attacks)
        {
            if(attack != activeAttack)
            {
                availableAttacks.Add(attack);
            }
        }

        int randomIndex = Random.Range(0, availableAttacks.Count);
        print(randomIndex);
        activeAttack = availableAttacks[randomIndex];
        activeAttack.UpdateActiveSprite();
        changedWeapon.Raise();

        availableAttacks.Clear();
    }
}
