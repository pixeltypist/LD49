using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Rigidbody2D rb;
    public GameEvent passedThroughPortal;
    Doorway doorway;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        doorway = GetComponent<Doorway>();
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") & doorway.isPortal)
        {
            passedThroughPortal.Raise();
            doorway.hasBeenPortalBefore = true;
        }
    }

}
