using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room : MonoBehaviour
{
    [SerializeField]GameObject topDoor, rightDoor, bottomDoor, leftDoor;
    public List<GameObject> doorways;
    [SerializeField]List<GameObject> enemySpawnPoints;
    public Dictionary<GameObject, room> neighbors = new Dictionary<GameObject, room>(); // links the doorway to the room
    Dictionary<string, GameObject> doorwayTranslators = new Dictionary<string, GameObject>(); // stores the string to be passed to set a doorway
    //public List<GameObject> linkedRooms;
    public bool playerLeavingRoom = false;

    bool isDictionaryInitialized = false;

    public TransformHold player;

    //these two bools are used to make sure the map UI is up to date 
    public bool playerInRoom = false;
    public bool playerHasVisitedRoom = false; 
    public GameEvent changedRooms;
    void Start()
    {
        InitializeDictionary();
    }

    public void LinkRooms(string sPosition, GameObject neighbor)
    {
        print(this.gameObject.name);
        if(!isDictionaryInitialized)
            InitializeDictionary();

        if(doorwayTranslators.ContainsKey(sPosition))
        {
            GameObject objKey = doorwayTranslators[sPosition];
            neighbors[objKey] = neighbor.GetComponent<room>();
            //linkedRooms.Add(neighbor);
        }
            
        else   
            print("Input wrong string key");
        
        //print(neighbor.GetComponent<Transform>());
    }

    void InitializeDictionary()
    {
        doorwayTranslators.Clear();
        doorwayTranslators.Add("Top", topDoor);
        doorwayTranslators.Add("Right", rightDoor);
        doorwayTranslators.Add("Bottom", bottomDoor);
        doorwayTranslators.Add("Left", leftDoor);
        isDictionaryInitialized = true;
    }

    public void SpawnPlayer()
    {
        print("Trying to spawn player");

        if(!playerHasVisitedRoom)
            playerHasVisitedRoom = true;

        player.objTransform.gameObject.transform.position = new Vector3(transform.position.x + 3, transform.position.y, 0);
        playerInRoom = true;
        changedRooms.Raise();
    }

    public void PickRandomDoor()
    {
        List<GameObject> availableRandomDoors = new List<GameObject>();
        foreach(var door in doorways)
        {
            if(door.GetComponent<doorway>().pickedAsRandomDoor)
                door.GetComponent<doorway>().pickedAsRandomDoor = false;

            if(!door.GetComponent<doorway>().usedAsRandomDoor)
            {
                availableRandomDoors.Add(door);
            }
        }
        if(availableRandomDoors.Count <=1)
        {
            if(availableRandomDoors.Count == 1)
            {
                availableRandomDoors[0].GetComponent<doorway>().pickedAsRandomDoor = true;
                availableRandomDoors[0].GetComponent<doorway>().UpdateColor();
                return;
            }

            if(availableRandomDoors.Count == 0)
            {
                return;
            }
                
        }

        foreach(var door in availableRandomDoors)
        {
            door.GetComponent<doorway>().potentialRandomDoor = true;
        }

        int indexOfRandomDoor = Random.Range(0, availableRandomDoors.Count);
        availableRandomDoors[indexOfRandomDoor].GetComponent<doorway>().pickedAsRandomDoor = true;

        foreach(var door in doorways)
        {
            door.GetComponent<doorway>().UpdateColor();
        }
    }

    public void ResetDoors()
    {
        foreach(var door in doorways)
        {
            if(door.GetComponent<doorway>().potentialRandomDoor == true)
            {
                door.GetComponent<doorway>().potentialRandomDoor = false;
            }
            if(door.GetComponent<doorway>().pickedAsRandomDoor == true)
            {
                door.GetComponent<doorway>().pickedAsRandomDoor = false;
                door.GetComponent<SpriteRenderer>().color = Color.green;
            }
            door.GetComponent<doorway>().UpdateColor();
        }
    }

    

    
}
