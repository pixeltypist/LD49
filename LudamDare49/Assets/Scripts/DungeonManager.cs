using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public List<GameObject> roomSpawnPoints; //all rooms
    public GameObject teleportDestination;
    public GameObject player;
    int currentIndex;

    int projectedIndex;

    private void Start() 
    {
        TurnOffAllRooms();
        int randomIndex = Random.Range(0, roomSpawnPoints.Count);
        currentIndex = randomIndex;
        teleportDestination = roomSpawnPoints[randomIndex];
        player.transform.position = teleportDestination.GetComponent<TransformSetter>().transformHold.objTransform.position;
        teleportDestination.GetComponent<PlayerSpawnPoint>().TeleportedToRoom();
        roomSpawnPoints[currentIndex].GetComponent<PlayerSpawnPoint>().roomData.roomIsActive = true;
        //needs to determine where the exit is
        //change the whole room
        //change the adjacent room
        //update its icon
        //update the adjacent room's icon
    }

    void TurnOffAllRooms()
    {
        foreach(var room in roomSpawnPoints)
        { 
            room.GetComponent<PlayerSpawnPoint>().roomData.roomIsActive = false;
        }
    }
    public void SetTeleportPoint()
    {
        int randomIndex = Random.Range(0,roomSpawnPoints.Count);
        if(randomIndex == currentIndex)
        {
            randomIndex = Random.Range(0,roomSpawnPoints.Count);
        }
        projectedIndex = randomIndex;
        teleportDestination = roomSpawnPoints[projectedIndex];


    }

    public void Teleport()
    {
        player.transform.position = teleportDestination.GetComponent<TransformSetter>().transformHold.objTransform.position;
        teleportDestination.GetComponent<PlayerSpawnPoint>().TeleportedToRoom();
        roomSpawnPoints[currentIndex].GetComponent<PlayerSpawnPoint>().roomData.roomIsActive = false;
        roomSpawnPoints[projectedIndex].GetComponent<PlayerSpawnPoint>().roomData.roomIsActive = true;

        currentIndex = projectedIndex;
    }
}
