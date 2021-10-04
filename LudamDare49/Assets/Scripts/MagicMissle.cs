using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : MonoBehaviour
{
    public FloatVariable speed;
    public FloatVariable damage;
    Rigidbody2D rb;

    Vector2 trajectory;
    //string boolToSetForAnim;
    Animator anim;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void SetTrajectory(Vector2 _trajectory, string boolToSet)
    {
        trajectory = _trajectory;
        anim.SetBool(boolToSet, true);
    }
    private void FixedUpdate() {
        Shoot(trajectory);
    }

    public void Shoot(Vector2 _trajectory)
    {
        rb.MovePosition(rb.position + trajectory*speed.Value*Time.fixedDeltaTime);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("wallBounds"))
        {
            print("Hit wall");
            anim.SetBool("disintegrate", true);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealthManager>().Attacked(damage.Value);
            anim.SetBool("disintegrate", true);
        }
    }

    void Disintegrate(){
        Destroy(this.gameObject);
    }

}
