using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    public Portal portal;
    public bool isPortal;
    public GameObject activeRoom;

    public GameObject player;

    public List<GameObject> roomConnections;
    public List<Transform> roomEntryPoints;
    Rigidbody2D rb;
    public MapUI mapUI;

    //public bool isActive;
    //public DoorwayToggle doorwayToggle;

    public bool hasBeenPortalBefore;
    public bool pickedToBePortal;

    public Dictionary<GameObject, Transform> entryPoints = new Dictionary<GameObject, Transform>();
    //public Dictionary<RoomData, GameObject> rooms = new Dictionary<RoomData, GameObject>();

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        for(int i = 0; i<roomConnections.Count; i++)
        {
            entryPoints.Add(roomConnections[i], roomEntryPoints[i]);
            //rooms.Add(roomConnections[i].GetComponent<RoomData>(), roomConnections[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") & !pickedToBePortal)
        {
            SetActiveRoom();
            activeRoom.GetComponent<RoomController>().EnteredRoom();
            MovePlayerToEntryPoint();
            //roomConnection.GetComponent<RoomController>().EnteredRoom();
        }
    }

    void MovePlayerToEntryPoint()
    {
        player.transform.position = entryPoints[activeRoom].position;
    }

    void SetActiveRoom()
    {
        mapUI.doorwayLinks.Clear();
        foreach(var room in roomConnections)
        {
            room.GetComponent<RoomController>().roomData.roomIsActive = !room.GetComponent<RoomController>().roomData.roomIsActive; // dependent on whether or not it's been set properly in the first place
            if(room.GetComponent<RoomController>().roomData.roomIsActive)
            {
                activeRoom = room;
            }
            mapUI.doorwayLinks.Add(room.GetComponent<RoomController>().roomData);
        }

        mapUI.UpdateLinks();
        //foreach(var link in mapUI.doorwayLinks)
       /* {
            mapUI.UpdatePlayerPosition(link, link.roomIsActive);
        }*/
        
    }

   /*public void UpdateDoorState()
    {
        isActive = doorwayToggle.doorsAreActive;
    }*/
    
    public void SetAsPortal()
    {
        pickedToBePortal = true;
        //isPortal = true;

        /*print("SetAsPortal()");
        if(hasBeenPortalBefore)
        {
            isPortal = false;
            portal.enabled = false;
        }
        if(!hasBeenPortalBefore)
        {
            isPortal = true;
            portal.enabled = true;
            hasBeenPortalBefore = true;
        }*/
        
        
    }
}
