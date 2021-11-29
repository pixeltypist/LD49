using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileController : MonoBehaviour
{
    [SerializeField]FloatVariable speed;
    float damage, range;
    Rigidbody2D rb;
    Vector2 trajectory = new Vector2(0,0);
    Animator anim;
    bool misActive = false;
    LayerMask enemyLayer;
    Vector2 startPos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //startPos = this.transform;
    }

    public void SetTrajectory(Vector2 pTrajectory, LayerMask pEnemyLayer, float pDamage, float pRange)
    {
        trajectory = pTrajectory;
        anim = GetComponent<Animator>();
        SetAnimValues();
        damage = pDamage;
        range = pRange;
        enemyLayer = pEnemyLayer;
        startPos = new Vector2(transform.position.x, transform.position.y);
        misActive = true;
    }
    void FixedUpdate()
    {
        if(misActive)
        {
            Shoot();
        }
        if(DistanceCheck())
            anim.SetBool("disintegrate", true);
    }

    void Shoot()
    {
        rb.MovePosition(rb.position + trajectory * speed.Value * Time.fixedDeltaTime);
        if(CastRay() != null)
        {
            CastRay().GetComponent<EnemyHealthManager>().Attacked(damage);
            anim.SetBool("disintegrate", true);
        }
    }
    
    void SetAnimValues()
    {
        anim.SetFloat("moveX", trajectory.x);
        anim.SetFloat("moveY", trajectory.y);
    }

    GameObject CastRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), trajectory, 0.1f, enemyLayer);

        if(hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    bool DistanceCheck()
    {
        Vector2 distance = new Vector2(startPos.x - transform.position.x, startPos.y - transform.position.y);
        float distanceTravelled = distance.magnitude;
        if(distanceTravelled >= range)
            return true;
        else
            return false;
    }

    void Disintegrate()
    {
        Destroy(this.gameObject);
    }
}
