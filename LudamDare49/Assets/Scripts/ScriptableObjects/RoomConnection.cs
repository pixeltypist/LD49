using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "RoomConnection", menuName = "ScriptableObjects/Room Connection")]
public class RoomConnection : ScriptableObject
{
    public GameObject roomController;
}
