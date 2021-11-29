using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class playerAttackBase_v2 : ScriptableObject
{
    public float damage;
    public float cooldown, chargeTime;
    public float range;
    public LayerMask enemyLayer;
    public TransformHold player;
    public Sprite attackSprite;
    public SpriteHolder spriteHolder;
    public virtual void SwordSwing(Animator anim)
    {
        
    }

    public virtual void HeavySword()
    {

    }
    public virtual void ShootMissile()
    {
        
    }

    public virtual void CastAreaOfEffect()
    {

    }

    public GameObject FindEnemy()
    {
        Vector2 origin = new Vector2(player.objTransform.position.x, player.objTransform.position.y);
        Vector2 direction = DetermineDirection();
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, range, enemyLayer);

        if(hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    public Vector2 DetermineDirection()
    {
        GameObject playerObject = player.objTransform.gameObject;
        float directionX = playerObject.GetComponent<playerMovement_v2>().lastMovedX;
        float directionY = playerObject.GetComponent<playerMovement_v2>().lastMovedY;
        Vector2 direction = new Vector2(directionX, directionY);
        return direction;
    }

    public void UpdateActiveSprite()
    {
        spriteHolder.SetSprite(attackSprite);
    }
   
}
