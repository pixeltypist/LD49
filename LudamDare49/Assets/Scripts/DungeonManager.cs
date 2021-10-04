using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public List<GameObject> roomSpawnPoints; //all rooms

    public GameObject finalRoomSpawnPoint;
    public GameObject teleportDestination;
    public GameObject player;
    int currentIndex;

    int projectedIndex;
    public MapUI mapUI;

    private void Start() 
    {
        mapUI.InitializeDictionary();
        currentIndex = 12;
        TurnOffAllRooms();
        //int randomIndex = Random.Range(0, roomSpawnPoints.Count);
        //currentIndex = randomIndex;
        teleportDestination = roomSpawnPoints[currentIndex];
        player.transform.position = teleportDestination.GetComponent<TransformSetter>().transformHold.objTransform.position;
        teleportDestination.GetComponent<PlayerSpawnPoint>().TeleportedToRoom();
        roomSpawnPoints[currentIndex].GetComponent<PlayerSpawnPoint>().roomData.roomIsActive = true;
        //print("Update map from DungeonManager start");
        //mapUI.UpdatePlayerPosition(roomSpawnPoints[currentIndex].GetComponent<PlayerSpawnPoint>().roomData, true);
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
        mapUI.UpdatePlayerPosition(roomSpawnPoints[currentIndex].GetComponent<PlayerSpawnPoint>().roomData, false);

        roomSpawnPoints[projectedIndex].GetComponent<PlayerSpawnPoint>().roomData.roomIsActive = true;
        mapUI.UpdatePlayerPosition(roomSpawnPoints[projectedIndex].GetComponent<PlayerSpawnPoint>().roomData, true);

        currentIndex = projectedIndex;
    }

    public void ResetDungeon()
    {
        mapUI.ResetDungeonMap();
        ResetRoomData();
        currentIndex = 12;
        TurnOffAllRooms();
        finalRoomSpawnPoint.GetComponent<PlayerSpawnPoint>().roomData.roomIsActive = false;
        //finalRoomSpawnPoint.GetComponent<RoomController>().TurnOffWalls();
        teleportDestination = roomSpawnPoints[currentIndex];
        roomSpawnPoints[currentIndex].GetComponent<PlayerSpawnPoint>().roomData.roomIsActive = true;
        player.transform.position = teleportDestination.GetComponent<TransformSetter>().transformHold.objTransform.position;
        teleportDestination.GetComponent<PlayerSpawnPoint>().TeleportedToRoom();
        

        //play some kind of animation?
    }

    void ResetRoomData()
    {
        foreach(var spawnPoint in roomSpawnPoints)
        {
            GameObject hold = spawnPoint.GetComponent<PlayerSpawnPoint>().roomController;
            hold.GetComponent<RoomController>().enteredOnce = false; 

            //reset sprites, too
            hold.GetComponent<RoomController>().TurnOffWalls();
        }
    }
}
