using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 cameraPosition = new Vector3(0, 0, -10);

    //���� �߾Ӱ�
    [SerializeField] private Vector2 center;
    //���� ������
    [SerializeField] private Vector2 mapSize;

    //ī�޶� �÷��̾��� �����Ӻ��� ������ �÷��̾ ���� ���� �̵��ӵ�
    [SerializeField] private float cameraMoveSpeed = 5;
    private float height;
    private float width;

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        LimitCameraArea();
    }

    private void LimitCameraArea()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + cameraPosition, Time.deltaTime * cameraMoveSpeed);

        float lx = mapSize.x - width;
        float ly = mapSize.y - height;

        float clampX = Mathf.Clamp(transform.position.x, center.x - lx, center.x + lx);
        float clampY = Mathf.Clamp(transform.position.y, center.y - ly, center.y + ly);

        transform.position = new Vector3(clampX, clampY, cameraPosition.z);
    }
}
