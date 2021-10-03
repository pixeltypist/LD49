using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
   public GameObject roomController;
   public RoomData roomData;

   public void TeleportedToRoom()
   {
       roomController.GetComponent<RoomController>().EnteredRoom();
       //roomController.GetComponent<RoomController>().roomIsActive = true;
   }
}
