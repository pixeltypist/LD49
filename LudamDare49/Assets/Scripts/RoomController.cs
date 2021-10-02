using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomController : MonoBehaviour
{
    public CinemachineConfiner confiner;
    public CompositeCollider2D cameraBounds;
    public RoomData roomData;
    public EnemyList enemyList;

    public void EnteredRoom()
    {
        roomData.enemies.Clear();
        confiner.m_BoundingShape2D = cameraBounds;
        PopulateEnemies();
        ToggleDoorways();
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
                roomData.enemies.Add(instantiatedEnemy);
            }
        }
    }
    void ToggleDoorways()
    {
        roomData.doorwayToggle.ToggleDoors();
    }
    public void AllEnemiesDefeated()
    {
        confiner.m_BoundingShape2D = null;
        ToggleDoorways();

    }

    private void Update() {
        if (!CheckForLiveEnemies())
        {
            AllEnemiesDefeated();
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

    bool CheckForLiveEnemies()
    {
        if (roomData.enemies.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
