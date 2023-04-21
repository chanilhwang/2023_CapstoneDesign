using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using System.Security.Cryptography;
using UnityEngine.Tilemaps;

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
    public int currRoomCnt = 0;
    public int xDistance = 18;
    public int yDistance = 10;
    private int iAroundcnt = 0;

    public GameObject exit;
    public GameObject BossRoom;
    public GameObject StoreRoom;
    public GameObject StartRoom;


    public GameObject[] Doors;

    public GameObject[] Rooms;

    public GameObject[] BackGroundTiles;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;

    public List<Vector3> rlist = new List<Vector3>();

    public List<RoomInfo> validRoomList = new List<RoomInfo>();
    public List<GameObject> RoomList;
    private Transform RoomHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Renderer render = RoomList[0].transform.Find("Sky01").gameObject.GetComponent<Renderer>();

            Debug.Log(render.bounds.size.x);
            Debug.Log(render.bounds.size.y);

        }
    }
    

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

        StartRoomSet();
        CreateRoom();
        BossRoomSet();

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
    void StartRoomSet()
    {
        GameObject toInstantiate = StartRoom;
        GameObject instance = Instantiate(toInstantiate, new Vector3(0f, 0f, 0f), Quaternion.identity);
        instance.transform.SetParent(RoomHolder);
        RoomList.Add(instance);
        rlist.Add(instance.transform.position);
        ++currRoomCnt;
        AddRightRoom(0);
        AddRightRoom(1);
    }

    void BossRoomSet()
    {
        Renderer render = RoomList[currRoomCnt-1].transform.Find("Sky01").gameObject.GetComponent<Renderer>();
        GameObject toInstantiate = BossRoom;
        GameObject instance = Instantiate(toInstantiate, RoomList[currRoomCnt - 1].transform.position + new Vector3(render.bounds.size.x+1f, 0f, 0f), Quaternion.identity);
        instance.transform.SetParent((RoomList[currRoomCnt - 1]).transform);
        RoomList.Add(instance);
        rlist.Add(instance.transform.position);
        ++currRoomCnt;
    }
    //Renderer render = RoomList[0].transform.Find("Sky01").gameObject.GetComponent<Renderer>();

    //        Debug.Log(render.bounds.size.x);
    //        Debug.Log(render.bounds.size.y);

    void AddRightRoom(int listidx)
    {
        GameObject toInstantiate = Rooms[Random.Range(0, Rooms.Length)];
        Renderer render = RoomList[listidx].transform.Find("Sky01").gameObject.GetComponent<Renderer>();
        if (iAroundcnt >= 4)
            return;
        if (rlist.Contains(RoomList[listidx].transform.position + new Vector3(render.bounds.size.x, 0f, 0f)))
            return;
        GameObject instance = Instantiate(toInstantiate, RoomList[listidx].transform.position + new Vector3(render.bounds.size.x, 0f, 0f), Quaternion.identity);
        instance.transform.SetParent((RoomList[listidx]).transform);
        RoomList.Add(instance);
        rlist.Add(instance.transform.position);
        //NextRoomCheck(listidx);

        ++currRoomCnt;
        ++iAroundcnt;
    }
    void AddLeftRoom(int listidx)
    {
        GameObject toInstantiate = Rooms[Random.Range(0, Rooms.Length)];
        Renderer render = RoomList[listidx].transform.Find("Sky01").gameObject.GetComponent<Renderer>();
        if (iAroundcnt >= 4)
            return;
        if (rlist.Contains(RoomList[listidx].transform.position + new Vector3(-render.bounds.size.x, 0f, 0f)))
            return;
        GameObject instance = Instantiate(toInstantiate, RoomList[listidx].transform.position + new Vector3(-render.bounds.size.x, 0f, 0f), Quaternion.identity);
        instance.transform.SetParent((RoomList[listidx]).transform);
        RoomList.Add(instance);
        rlist.Add(instance.transform.position);
        //NextRoomCheck(listidx);

        ++currRoomCnt;
        ++iAroundcnt;
    }

    void AddUpRoom(int listidx)
    {
        GameObject toInstantiate = Rooms[Random.Range(0, Rooms.Length)];
        Renderer render = RoomList[listidx].transform.Find("Sky01").gameObject.GetComponent<Renderer>();
        if (iAroundcnt >= 4)
            return;
        if (rlist.Contains(RoomList[listidx].transform.position + new Vector3(0f, render.bounds.size.y, 0f)))
            return;
        GameObject instance = Instantiate(toInstantiate, RoomList[listidx].transform.position + new Vector3(0f, render.bounds.size.y, 0f), Quaternion.identity);
        instance.transform.SetParent((RoomList[listidx]).transform);
        RoomList.Add(instance);
        rlist.Add(instance.transform.position);
        //NextRoomCheck(listidx);

        ++currRoomCnt;
        ++iAroundcnt;
    }
    void AddDownRoom(int listidx)
    {
        GameObject toInstantiate = Rooms[Random.Range(0, Rooms.Length)];
        Renderer render = RoomList[listidx].transform.Find("Sky01").gameObject.GetComponent<Renderer>();
        if (iAroundcnt >= 4)
            return;
        if (rlist.Contains(RoomList[listidx].transform.position + new Vector3(0f, -render.bounds.size.y, 0f)))
            return;
        GameObject instance = Instantiate(toInstantiate, RoomList[listidx].transform.position + new Vector3(0f, -render.bounds.size.y, 0f), Quaternion.identity);
        instance.transform.SetParent((RoomList[listidx]).transform);
        RoomList.Add(instance);
        rlist.Add(instance.transform.position);
        //NextRoomCheck(listidx);

        ++currRoomCnt;
        ++iAroundcnt;
    }

    void CreateRoom()
    {
        for(int i=2; i< columns ; ++i)
        {
            if(currRoomCnt>= RoomCount.maximum) { break; }
            iAroundcnt = 0;
            for(int j = 0; j <= 2; ++j)
            {
                if (0 == Random.Range(0, 4))
                {
                    AddUpRoom(i);
                }
                if (1 == Random.Range(0, 4))
                {
                    AddRightRoom(i);
                }
                if (2 == Random.Range(0, 4))
                {
                    AddDownRoom(i);
                }
                if (3 == Random.Range(0, 4))
                {
                    AddLeftRoom(i);
                }
            }

            if(currRoomCnt >= RoomCount.minimum && currRoomCnt <= RoomCount.maximum ) { break; }

        }
        NextRoomCheck2();
    }

    void AddRoom(int listidx,Vector3 dir)
    {
        if (iAroundcnt >= 4)
            return;
        if (rlist.Contains(RoomList[listidx].transform.position + dir))
            return;
        GameObject toInstantiate = Rooms[Random.Range(0, Rooms.Length)];
        GameObject instance = Instantiate(toInstantiate, RoomList[listidx].transform.position+dir, Quaternion.identity);
        instance.transform.SetParent((RoomList[listidx]).transform);
        RoomList.Add(instance);
        rlist.Add(instance.transform.position);
        //NextRoomCheck(listidx);
        
        ++currRoomCnt;
        ++iAroundcnt;
    }

    void NextRoomCheck2()
    {
        foreach (GameObject Room in RoomList)
        {
            int dooridx;
            if (rlist.Contains(Room.transform.position + new Vector3(0f, 10f, 0f)))
            {
                dooridx = 0;
                AddDoor2(Room, dooridx);
            }
            if (rlist.Contains(Room.transform.position + new Vector3(18f, 0f, 0f)))
            {
                dooridx = 1;
                AddDoor2(Room, dooridx);
            }
            if (rlist.Contains(Room.transform.position + new Vector3(0f, -10f, 0f)))
            {
                dooridx = 2;
                AddDoor2(Room, dooridx);
            }
            if (rlist.Contains(Room.transform.position + new Vector3(-18f, 0f, 0f)))
            {
                dooridx = 3;
                AddDoor2(Room, dooridx);
            }
        }
    }

    void AddDoor2(GameObject Room, int dooridx)
    {
        switch (dooridx)
        {
            case 0:
                {
                    GameObject toInstantiate = Doors[0];
                    GameObject instance = Instantiate(toInstantiate, Room.transform.position, Quaternion.identity);
                    instance.transform.SetParent((Room).transform);
                    return;
                }

            case 1:
                {
                    GameObject toInstantiate = Doors[1];
                    GameObject instance = Instantiate(toInstantiate, Room.transform.position, Quaternion.identity);
                    instance.transform.SetParent((Room).transform);
                    return;

                }
            case 2:
                {
                    GameObject toInstantiate = Doors[0];
                    GameObject instance = Instantiate(toInstantiate, Room.transform.position + new Vector3(0f, -9f, 0f), Quaternion.identity);
                    instance.transform.SetParent((Room).transform);
                    return;

                }
            case 3:
                {
                    GameObject toInstantiate = Doors[1];
                    GameObject instance = Instantiate(toInstantiate, Room.transform.position + new Vector3(-17f, 0f, 0f), Quaternion.identity);
                    instance.transform.SetParent((Room).transform);
                    return;
                }
        }
    }


    void AddDoor(int listidx,int dooridx)
    {
        switch (dooridx)
        {
            case 0:
                {
                    GameObject toInstantiate = Doors[0];
                    GameObject instance = Instantiate(toInstantiate, RoomList[listidx].transform.position, Quaternion.identity);
                    instance.transform.SetParent((RoomList[listidx]).transform);
                    return;
                }
      
            case 1:
                {
                    GameObject toInstantiate = Doors[1];
                    GameObject instance = Instantiate(toInstantiate, RoomList[listidx].transform.position, Quaternion.identity);
                    instance.transform.SetParent((RoomList[listidx]).transform);
                    return;
                    
                }
            case 2:
                {
                    GameObject toInstantiate = Doors[0];
                    GameObject instance = Instantiate(toInstantiate, RoomList[listidx].transform.position + new Vector3(0f,-9f,0f), Quaternion.identity);
                    instance.transform.SetParent((RoomList[listidx]).transform);
                    return;
                    
                }
            case 3:
                {
                    GameObject toInstantiate = Doors[1];
                    GameObject instance = Instantiate(toInstantiate, RoomList[listidx].transform.position + new Vector3(-17f, 0f, 0f), Quaternion.identity);
                    instance.transform.SetParent((RoomList[listidx]).transform);
                    return;
                }
        }
    }

    void NextRoomCheck(int listidx)
    {
        int dooridx;
        if (rlist.Contains(RoomList[listidx].transform.position + new Vector3(0f, 10f, 0f)))
        {
            dooridx = 0;
            AddDoor(listidx, dooridx);
        }
        if (rlist.Contains(RoomList[listidx].transform.position + new Vector3(18f, 0f, 0f)))
        {
            dooridx = 1;
            AddDoor(listidx, dooridx);
        }
        if (rlist.Contains(RoomList[listidx].transform.position + new Vector3(0f, -10f, 0f)))
        {
            dooridx = 2;
            AddDoor(listidx, dooridx);
        }
        if (rlist.Contains(RoomList[listidx].transform.position + new Vector3(-18f, 0f, 0f)))
        {
            dooridx = 3;
            AddDoor(listidx, dooridx);
        }
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
