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

    public virtual void SwordSwing()
    {

    }
    public virtual void MagicMissle(Vector2 target, Transform attacker, Transform attackPoint)
    {

    }

    public virtual void AOE(TransformHold playerPos, GameObject playerObj)
    {

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
