using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorway : MonoBehaviour
{
    room parentRoom;
    BoxCollider2D col;

    public GameEvent playerThroughDoor, playerThroughRandomDoor;
    public bool passedThroughThisDoor = false;
    public bool potentialRandomDoor = true;
    public bool pickedAsRandomDoor = false;
    public bool usedAsRandomDoor = false;

    void Start() 
    {
        parentRoom = GetComponentInParent<room>();
        col = GetComponent<BoxCollider2D>();
    }

    public void UpdateColor()
    {
        if(potentialRandomDoor)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        
        if(pickedAsRandomDoor)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if(usedAsRandomDoor)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            print("Player Detected");
            parentRoom.playerLeavingRoom = true;
            parentRoom.playerInRoom = false;
            if(pickedAsRandomDoor)
            {
                potentialRandomDoor = false;
                usedAsRandomDoor = true;
                playerThroughRandomDoor.Raise();
            }
            else
            {
                passedThroughThisDoor = true;
                playerThroughDoor.Raise();
            }
            
        }
    }

}
