using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransitionRight : MonoBehaviour
{
    // ī�޶� ��ũ��Ʈ ����
    private MainCameraController cam;
    // ���ο� �� ī�޶� �ּ�, �ִ� ���� ����
    [SerializeField] Vector2 newMinCameraBoundary;
    [SerializeField] Vector2 newMaxCameraBoundary;
    // �÷��̾ ���ο� ������ �̵��� �� ��ġ ����.
    [SerializeField]
    Vector2 playerPosOffset;
    [SerializeField]
    Transform exitPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
