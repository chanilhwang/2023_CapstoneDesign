using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class RoomManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 100;
    public int rows = 100;
    public Count RoomCount = new Count(5, 9);
    private int currRoomCnt = 0;
    public int xDistance = 18;
    public int yDistance = 10;


    public GameObject exit;
    public GameObject BossRoom;
    public GameObject ShopRoom;
    public GameObject StartRoom;
    public GameObject[] Rooms;

    public GameObject[] BackGroundTiles;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;

    public List<RoomInfo> validRoomList = new List<RoomInfo> ();
    public List<GameObject> RoomList;
    private Transform RoomHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    

    void InitializeList()
    {
        gridPositions.Clear();

        for (int i = 0; i < columns ; ++i)
        {
            for (int j = 0; j < rows ; ++j)
            {
                gridPositions.Add(new Vector3(i, j, 0f));
            }
        }
    }

    void RoomSetup()
    {
        RoomHolder = new GameObject("Room").transform;

        GameObject toInstantiate = StartRoom;
        GameObject instance = Instantiate(toInstantiate, new Vector3(0f, 0f, 0f), Quaternion.identity);
        instance.transform.SetParent(RoomHolder);
        RoomList.Add(instance);
        ++currRoomCnt;
        CreateRoom();

        //for (int i = -1; i < columns + 1; ++i)
        //{
        //    for (int j = -1; j < rows + 1; ++j)
        //    {
        //        GameObject toInstantiate = BackGroundTiles[0];

        //        if (j == 0)
        //            toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

        //        if (i == -1 || i == columns || j == -1 || j == rows)
        //        {
        //            toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
        //        }

        //        GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity);


        //        instance.transform.SetParent(RoomHolder);
        //    }
        //}

    }

    
    void CreateRoom()
    {
        int i = 0;
        int iAroundcnt = 0;
        //currRoomCnt != RoomCount.maximum
        while (iAroundcnt<5)
        {
            while (0 == iAroundcnt)
            {
                if (0 == Random.Range(0, 4))
                {
                    AddRoom(i, new Vector3(0f, 10f, 0f));
                    ++iAroundcnt;
                }
                if (1 == Random.Range(0, 4))
                {
                    AddRoom(i, new Vector3(18f, 0f, 0f));
                    ++iAroundcnt;
                }
                if (2 == Random.Range(0, 4))
                {
                    AddRoom(i, new Vector3(0f, -10f, 0f));
                    ++iAroundcnt;
                }
                if (3 == Random.Range(0, 4))
                {
                    AddRoom(i, new Vector3(-18f, 0f, 0f));
                    ++iAroundcnt;
                }
            }
            ++i;
        }
    }

    void AddRoom(int listidx,Vector3 dir)
    {
        //Collider[] a = Physics.OverlapBox(RoomList[listidx].transform.position + dir,StartRoom.transform.localScale/2);
        //if(a == null)
        //{ return; }
        GameObject toInstantiate = Rooms[Random.Range(0, Rooms.Length)];
        GameObject instance = Instantiate(toInstantiate, RoomList[listidx].transform.position+dir, Quaternion.identity);
        instance.transform.SetParent((RoomList[listidx]).transform);
        RoomList.Add(instance);
        ++currRoomCnt;
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0,gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];   
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range (minimum, maximum + 1);

        for (int i = 0; i < objectCount; ++i)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice,randomPosition,Quaternion.identity);
        }
    }

    public void SetupScene()
    {
        RoomSetup();

        InitializeList();

        //LayoutObjectAtRandom(wallTiles,wallCount.minimum,wallCount.maximum);

    }


}
