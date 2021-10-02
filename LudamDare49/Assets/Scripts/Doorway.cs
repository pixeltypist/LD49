using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    public Portal portal;
    public bool isPortal;
    public GameObject roomConnection;

    Rigidbody2D rb;

    public bool isActive;
    public DoorwayToggle doorwayToggle;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(isActive & other.CompareTag("Player"))
        {
            roomConnection.GetComponent<RoomController>().EnteredRoom();
        }
    }

    public void UpdateDoorState()
    {
        isActive = doorwayToggle.doorsAreActive;
    }

}
