using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorwayToggle", menuName = "ScriptableObjects/Doorway Toggle")]
public class DoorwayToggle : ScriptableObject
{
    public bool doorsAreActive;
    public GameEvent toggleDoors;

    public void ToggleDoors()
    {
        doorsAreActive = !doorsAreActive;
        toggleDoors.Raise();
    }
}
