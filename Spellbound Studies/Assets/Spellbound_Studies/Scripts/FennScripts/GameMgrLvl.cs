using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgrLvl : MonoBehaviour
{
     public static GameMgr inst;
    public List<int> roomIndexes;
    public List<Room> rooms;
    public int numRooms;
    public int currentRoom;
    bool loadNext;
    public GameObject player;
    private void Awake()
    {
        // inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        roomIndexes = new List<int>();
        for(int i = 0; i < numRooms-1; i++)
        {
            int roomIndex = Random.Range(0, rooms.Count);
            roomIndexes.Add(roomIndex);
        }
        roomIndexes.Add(rooms.Count - 1);
        currentRoom = -1;
        loadNext = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadNext)
        {
            NextRoom();
            loadNext = false;
        }
    }

    public void NextRoom()
    {
        currentRoom++;
        rooms[roomIndexes[currentRoom]].EnterRoom(player.transform, 1, 3, 2);
    }
}
