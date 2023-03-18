using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : Singleton<RoomController>
{
    public string GlobalRoomTitle = "Basement";

    public RoomInfo currentLoadRoomData;
    public Room currRoom;

    public List<Room> loadedRooms = new List<Room>();

    public Material DefaultBackground;
    public Material VisitedBack;
    public Material currMaterial;


    public bool isLoadingRoom = false;

    public void CreateRoom()
    {
        isLoadingRoom = false;
        
        for(int i =0; i<transform.childCount; ++i)
            Destroy(transform.GetChild(i).gameObject);

        loadedRooms.Clear();

        Player.Instance.transform.position = new Vector3(0,0.5f,0);
        DungeonCrawlerController.Instance.CreateRoom();
        SetRoomPath();
        
    }

    void SetRoomPath()
    {
        if (isLoadingRoom)
            return;

        if(loadedRooms.Count > 0)
        {
            foreach(Room room in loadedRooms)
            {
                room.RemoveUncoonnectedWalls();
            }
            isLoadingRoom=true;
        }
    }

    public void LoadRoom(RoomInfo settingRoom)
    {
        if(DoseRoomExist(settingRoom.center_Position.x,settingRoom.center_Position.y,settingRoom.center_Position.z))
        {
            return;
        }
        string roomPreName = settingRoom.roomName;

        GameObject room = Instantiate(RoomPreFabsSet.Instance.roomPrefabs[roomPreName]);
        room.transform.position = new Vector3(
            (settingRoom.center_Position.x * room.transform.GetComponent<Room>().Width),
             settingRoom.center_Position.y,
            (settingRoom.center_Position.z * room.transform.GetComponent<Room>().Height)
);

        room.transform.localScale = new Vector3(
                    (room.transform.GetComponent<Room>().Width / 10),
                     1,
                    (room.transform.GetComponent<Room>().Height / 10)
        );
        room.transform.GetComponent<Room>().center_Position = settingRoom.center_Position;
        room.name = globalRoomTitle + "-" + settingRoom.roomName + " " + settingRoom.center_Position.x + ", " + settingRoom.center_Position.z;

        room.transform.GetComponent<Room>().roomName = settingRoom.roomName;
        room.transform.GetComponent<Room>().roomType = settingRoom.roomType;
        room.transform.GetComponent<Room>().roomId = settingRoom.roomID;
        room.transform.GetComponent<Room>().parent_Position = settingRoom.parent_Position;
        room.transform.GetComponent<Room>().mergeCenter_Position = settingRoom.mergeCenter_Position;
        room.transform.GetComponent<Room>().distance = settingRoom.distance;

        room.transform.parent = transform;

        loadedRooms.Add(room.GetComponent<Room>());
    }

    // �� ������ Ȥ�� ������ ���� ���� ��� ����ó��
    public bool DoesRoomExist(int x, int y, int z)
    {
        return loadedRooms.Find(item=>item.center_Position.x == x && item.center_Position.y == y && item.center_Position.z == z) != null;
    }

    public Room FindRoom(int x, int y, int z)
    {
        // List.Find : item ���� ���ǿ� �´� Room�� ã�� ��ȯ
        return loadedRooms.Find(item => item.center_Position.x == x && item.center_Position.y == y && item.center_Position.z == z);
    }

    // �ش� Room���� Player�� �ִ� ���� ��ȯ
    public void OnPlayerEnterRoom(Room room)
    {
        CameraFollow.Instance.currRoom = room;

        currRoom = room;

        for (int i = 0; i < loadedRooms.Count; i++)
        {
            if (room.parent_Position == loadedRooms[i].parent_Position)
                loadedRooms[i].childRooms.minimapUpdate();
        }
    }


}

