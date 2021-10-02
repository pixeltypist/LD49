using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "ScriptableObjects/Room Data")]
public class RoomData : ScriptableObject
{
   public DoorwayToggle doorwayToggle;
   public List<TransformHold> enemySpawnPoints;

   public Transform playerSpawnLocation;

   public List<GameObject> enemies;

}
