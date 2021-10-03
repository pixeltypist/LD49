using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomController : MonoBehaviour
{  
    public GameObject DungeonManager;
    public CinemachineConfiner confiner;
    public CompositeCollider2D cameraBounds;
    public RoomData roomData;
    public EnemyList enemyList;
    bool allEnemiesDead;
    public bool roomIsActive = false;
    bool toggledDoorAfterClear = false;

    bool enteredOnce;

    public List<BoxCollider2D> doorwayColliders;

    public List<GameObject> Doorways;
    List<GameObject> viablePortals;

    int portalIndex;

    private void Start() {
        TurnOffWalls();
        viablePortals = Doorways;
    }
    public void EnteredRoom()
    {
        print("Entered room (roomController)");
        roomData.enemies.Clear();
        confiner.m_BoundingShape2D = cameraBounds;
        PopulateEnemies();
        //ToggleDoorways();
        TurnOnWalls();
        if(enteredOnce)
        {
            allEnemiesDead = false;
            toggledDoorAfterClear = false;
        }
        
        if(!enteredOnce)
        {
            enteredOnce = true;
        }
        
    }

    void PopulateEnemies()
    {
        foreach (var enemySpawnPoint in roomData.enemySpawnPoints)
        {
            int roll = Random.Range(0,100);
            if(roll < 50)
            {
                int randomEnemy = Random.Range(0,enemyList.enemyList.Count);
                Vector3 spawnPoint = new Vector3(enemySpawnPoint.objTransform.position.x, enemySpawnPoint.objTransform.position.y, 0);
                GameObject instantiatedEnemy = Instantiate(enemyList.enemyList[randomEnemy], spawnPoint, Quaternion.identity);
                instantiatedEnemy.GetComponent<EnemyController>().SetRoomController(this);
                roomData.enemies.Add(instantiatedEnemy);
            }
        }
    }
    void ToggleDoorways()
    {
        roomData.doorwayToggle.ToggleDoors();
    }

    void TurnOnWalls()
    {
        print("TurnOnWalls");
        foreach(var door in doorwayColliders)
        {
            door.enabled = true;
        }
    }

    void TurnOffWalls()
    {
        print("TurnOffWalls");
        foreach(var door in doorwayColliders)
        {
            door.enabled = false;
        }
    }
    public void AllEnemiesDefeated()
    {
        DecideOnPortalDoorway();
        print("AllEnemiesDefeated()");
        //confiner.m_BoundingShape2D = null;
        ToggleDoorways();
        TurnOffWalls();
    }

    private void Update() {
        if(roomData.roomIsActive)
        {
            CheckForLiveEnemies();
            if (allEnemiesDead & !toggledDoorAfterClear)
            {
                AllEnemiesDefeated();
                toggledDoorAfterClear = true;
            }
        }
       
    }

    public void EnemyDied(GameObject _enemy)
    {
        foreach(var enemy in roomData.enemies)
        {
            if(_enemy == enemy)
            {
                roomData.enemies.Remove(enemy);
                return;
            }
        }
    }

    void CheckForLiveEnemies()
    {
        if (roomData.enemies.Count == 0)
        {
            allEnemiesDead = true;
        }
        else
        {
            allEnemiesDead = false;
        }
    }

    void DecideOnPortalDoorway()
    {            
        DetermineViablePortals();

        if(viablePortals.Count>0)
        {
            int portalIndex = Random.Range(0, viablePortals.Count);
            viablePortals[portalIndex].GetComponent<Doorway>().SetAsPortal();
            DungeonManager.GetComponent<DungeonManager>().SetTeleportPoint();
        }
        
    }


    void DetermineViablePortals()
    {
        if(viablePortals.Count>0)
        {
            foreach(var door in viablePortals)
            {
                if(door.GetComponent<Doorway>().hasBeenPortalBefore)
                {
                    if(door.GetComponent<Doorway>().portal.enabled | door.GetComponent<Doorway>().isPortal)
                    {
                        door.GetComponent<Doorway>().portal.enabled = false;
                        door.GetComponent<Doorway>().isPortal = false;
                    }
                    viablePortals.Remove(door);
                }
            }
        }
        
    }
}
