using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public List<Image> roomSprites;
    public List<RoomData> roomDatas;
    public Sprite playerLocation;
    public Sprite baseSprite;

    public Dictionary<RoomData, Image> UIConnections;

    public List<RoomData> doorwayLinks;

    private void Start() 
    {
        
    }

    public void InitializeDictionary()
    {
        UIConnections = new Dictionary<RoomData, Image>();
        for(int i=0; i<roomDatas.Count; i++)
        {
            print("Initializing dicitonary");
            UIConnections.Add(roomDatas[i], roomSprites[i]);
        }
    }

    public void RevealRoom(RoomData room)
    {
        UIConnections[room].enabled = false;
    }

    public void UpdatePlayerPosition(RoomData room, bool isThere)
    {
        if(isThere)
        {
            UIConnections[room].sprite = playerLocation;
            UIConnections[room].enabled = true;
        }
        else
        {
            UIConnections[room].enabled = false;
        }
    }

    public void ResetDungeonMap()
    {
        foreach(var sprite in roomSprites)
        {
            sprite.sprite = baseSprite;
            sprite.enabled = true;
        }
        //RevealRoom(doorwayLinks[12]);
    }

    public void UpdateLinks()
    {
        foreach(var link in doorwayLinks)
        {
            UpdatePlayerPosition(link, link.roomIsActive);
        }
    }
}
