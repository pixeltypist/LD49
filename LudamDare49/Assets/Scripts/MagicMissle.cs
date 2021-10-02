using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : MonoBehaviour
{
    public FloatVariable speed;
    public FloatVariable damage;
    Rigidbody2D rb;

    Vector2 trajectory;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTrajectory(Vector2 _trajectory)
    {
        trajectory = _trajectory;
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
            Disintegrate();
        }
        else if (other.CompareTag("Enemy"))
        {
            print("Hit an enemy");
        }
    }

    void Disintegrate(){
        Destroy(gameObject);
    }

}
