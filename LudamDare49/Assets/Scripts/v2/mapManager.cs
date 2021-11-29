using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Cinemachine;

public class mapManager : MonoBehaviour
{
    /*
        This manages everything regarding the map
            Placing tiles
            Creating and linking rooms
            Tracking player position
                When they move between rooms
                Which room they move to
                The placement of the camera bounds
    */

    [SerializeField]List<TileBase> tilesForUse;
        //0: bottomLeft
        //1: bottom
        //2: bottomRight
        //3: leftSide
        //4: center
        //5: rightSide
        //6: topLeft
        //7: top
        //8: topRight

    [SerializeField]List<GameObject> roomsMatchedWithTiles = new List<GameObject>();
        //0: bottomLeft
        //1: bottom
        //2: bottomRight
        //3: leftSide
        //4: center
        //5: rightSide
        //6: topLeft
        //7: top
        //8: topRight
    [SerializeField]Tilemap tilemap;
    [SerializeField]int widthAdjust, heightAdjust;
    [SerializeField]PolygonCollider2D cameraBounds;
    //[SerializeField]GameObject defaultRoom;
    public Vector3Int[] positions = new Vector3Int[16];
    TileBase[] tileArray = new TileBase[16];

    List<room> roomList = new List<room>();
    Dictionary<Vector3Int, GameObject> roomLocations = new Dictionary<Vector3Int, GameObject>();

    public TransformHold player;
    void Start()
    {
        PlaceTilesInital(); // places all 16 tiles; could be made more generic, but that's for me later >.>
        MoveCameraBounds(); // this sets the camera to the first room (right now, anyway)
        PlaceRooms();
        LinkRooms();
    }

    void PlaceTilesInital()
    {
        int counter = 0;
        int tileArrayIndex = 0;
        
        //TileBase[] tileArray = new TileBase[positions.Length];
        
        for(int i=0; i<4; i++)
        {
            for (int j=0; j<4; j++)
            {
                positions[counter] = new Vector3Int(i + widthAdjust*i, j + heightAdjust*j, 0);
                //tile checks
                if(i==0) // left
                {
                    if(j==0)
                    {
                        //bottomLeft
                        tileArrayIndex = 0;
                    }
                    else if(j==3)
                    {
                        //topLeft
                        tileArrayIndex = 6;
                    }
                    else
                    {
                        //leftSide
                        tileArrayIndex = 3;
                    }
                }
                else if(i==3) // right
                {
                    if(j==0)
                    {
                        //bottomRight
                        tileArrayIndex = 2;
                    }
                    else if(j==3)
                    {
                        //topRight
                        tileArrayIndex = 8;
                    }
                    else
                    {
                        //right
                        tileArrayIndex = 5;
                    }
                }
                else if(j==0) // bottom
                {
                    tileArrayIndex = 1;
                }
                else if(j==3) // top
                {
                    tileArrayIndex = 7;
                }
                else
                {
                    //center
                    tileArrayIndex = 4;
                }

                tileArray[counter] = tilesForUse[tileArrayIndex];
                //positionsAgain[i,j] = positions[counter];
                counter++;
            }
        }
        tilemap.SetTiles(positions, tileArray);
    }


    void MoveCameraBounds() // this will eventually adjust based on the room she shows up in
    {
        cameraBounds.GetComponent<Transform>().position = positions[0];
    }

    void PlaceRooms()
    {
        for(int i = 0; i<positions.Length; i++)
        {
            int roomIndex = 0;
            //find roomIndex to use
            for(int j = 0; j<tilesForUse.Count; j++)
            {
                if(tileArray[i]==tilesForUse[j])
                {
                    roomIndex = j;
                }
            }
            Vector3Int adjustedPosition = new Vector3Int(positions[i].x - 3, positions[i].y, 0);
            GameObject roomToSpawn = Instantiate(roomsMatchedWithTiles[roomIndex], adjustedPosition, Quaternion.identity);
            roomLocations.Add(positions[i], roomToSpawn);
            roomList.Add(roomToSpawn.GetComponent<room>());
        }
    }

    void LinkRooms()
    {
        foreach(var position in positions)
        {
            print("in foreach loop");
            //top room
            if(tilemap.HasTile(new Vector3Int(position.x, position.y + 11, position.z)))
            {
                //print("upper tile found");
                //print(roomLocations.ContainsKey(new Vector3Int(position.x, position.y + 11, position.z)));
                roomLocations[position].GetComponent<room>().LinkRooms("Top", roomLocations[new Vector3Int(position.x, position.y + 11, position.z)]);
            }

            //right room
            if(tilemap.HasTile(new Vector3Int(position.x + 15, position.y, position.z)))
            {
                //print("upper tile found");
                //print(roomLocations.ContainsKey(new Vector3Int(position.x, position.y + 11, position.z)));
                roomLocations[position].GetComponent<room>().LinkRooms("Right", roomLocations[new Vector3Int(position.x + 15, position.y, position.z)]);
            }

            //bottom room
            if(tilemap.HasTile(new Vector3Int(position.x, position.y - 11, position.z)))
            {
                //print("upper tile found");
                //print(roomLocations.ContainsKey(new Vector3Int(position.x, position.y + 11, position.z)));
                roomLocations[position].GetComponent<room>().LinkRooms("Bottom", roomLocations[new Vector3Int(position.x, position.y - 11, position.z)]);
            }
            //left room
            if(tilemap.HasTile(new Vector3Int(position.x - 15, position.y, position.z)))
            {
                //print("upper tile found");
                //print(roomLocations.ContainsKey(new Vector3Int(position.x, position.y + 11, position.z)));
                roomLocations[position].GetComponent<room>().LinkRooms("Left", roomLocations[new Vector3Int(position.x - 15, position.y, position.z)]);
            }
        }
    }

    public void MoveRooms()
    {
        print("Trying to move rooms");
        foreach(var rooms in roomList)
        {
            if(rooms.playerLeavingRoom == true)
            {
                print("Registered that the player is trying to leave room");
                foreach(var door in rooms.doorways)
                {
                    if (door.GetComponent<doorway>().passedThroughThisDoor)
                    {
                        //find linked room
                        print("Found doorway");
                        //move player to that room
                        rooms.neighbors[door.gameObject].SpawnPlayer();
                        //move camera
                        cameraBounds.GetComponent<Transform>().position = player.objTransform.gameObject.transform.position;
                        rooms.neighbors[door.gameObject].PickRandomDoor();
                        //reset bool
                        door.GetComponent<doorway>().passedThroughThisDoor = false;
                    }
                }
                //reset bool
                rooms.playerLeavingRoom = false;
                rooms.ResetDoors();
            }
        }
    }

    public void PlacePlayerInRandomRoom()
    {
        foreach(var rooms in roomList)
        {
            if(rooms.playerLeavingRoom == true)
            {
                print("Registered that the player is trying to leave room");
                foreach(var door in rooms.doorways)
                {
                    if (door.GetComponent<doorway>().usedAsRandomDoor)
                    {
                        //find linked room
                        print("using random door");
                        //pick random room
                        int randomIndex = Random.Range(0, roomList.Count);
                        //make sure player doesn't spawn in same room again
                        if(roomList[randomIndex].playerLeavingRoom == true)
                        {
                            if(randomIndex >= roomList.Count)
                                randomIndex--;
                            else
                                randomIndex++;
                        }
                        roomList[randomIndex].GetComponent<room>().SpawnPlayer();
                        //move camera
                        cameraBounds.GetComponent<Transform>().position = player.objTransform.gameObject.transform.position;
                        roomList[randomIndex].GetComponent<room>().PickRandomDoor();
                        //reset bool
                        //door.GetComponent<doorway>().passedThroughThisDoor = false;
                    }
                }
                //reset bool
                rooms.playerLeavingRoom = false;
                rooms.ResetDoors();
            }
        }
    }
}
