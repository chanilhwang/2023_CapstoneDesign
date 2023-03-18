using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class RoomInfo
{
    public string roomID;
    public string roomName;
    public string roomType;

    // ���� ��(����)�� ��ġ
    public Vector3Int center_Position;
    // �θ� ���� ��ġ
    public Vector3Int parent_Position;
    // �ش� ��(����)�� �߾� ��ġ
    public Vector3 mergeCenter_Position;
    // �ش� ���� ���� ����(true : �� ����, false : ���)
    public bool isValidRoom;
    // ���� �濡�� ���� �ش� ������� �Ÿ�
    public int distance;

}
