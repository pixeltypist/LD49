using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "ScriptableObjects/EnemyList")]
public class EnemyList : ScriptableObject
{
    public List<GameObject> enemyList;
}
