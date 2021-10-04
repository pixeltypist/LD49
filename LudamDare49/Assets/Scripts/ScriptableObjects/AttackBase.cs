using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : ScriptableObject 
{
    public float damage;
    public float cooldown;
    public float range;
    public bool isActive;
    public Sprite sprite;
    public SpriteHolder spriteHolderForUI;
    public GameEvent UIupdate;

    public virtual GameObject SwordSwing(bool playerFacingY)
    {
        return null;
    }
    public virtual void MagicMissle(Vector2 target, Transform attacker, Transform attackPoint)
    {

    }

    public virtual GameObject AOE()
    {
        return null;
    }


    public void ChangeOut()
    {
        isActive = !isActive;
        if(isActive)
        {
            spriteHolderForUI.SetSprite(sprite);
            UIupdate.Raise();
        }
    }
}
