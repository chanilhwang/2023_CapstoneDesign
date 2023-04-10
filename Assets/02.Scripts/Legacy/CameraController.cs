using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, -10);
    [SerializeField] private float cameraMoveSpeed = 5;
    //카메라 안의 플레이어가 움직여도 카메라가 같이 움직이지 않는 범위
    [SerializeField] private Vector2 rangeSize;
    [SerializeField] Grid grid;
    //카메라가 움직일 떄 몇 초 이내로 움직여야 부드럽게 보이는지 나타내는 시간
    [SerializeField] private float smoothTime = 0.3f;

    private Vector2 rangeOffset;

    private Bounds worldBounds;

    private float camHalfHeight;
    private float camHalfWidth;

    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rangeOffset = rangeSize / 2f;
    }

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        if(grid == null)
        {
            grid = FindFirstObjectByType<Grid>();
        }

        worldBounds = new Bounds(grid.transform.position, Vector3.zero);

        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;

        foreach(Transform child in grid.transform)
        {
            Tilemap tilemap = child.GetComponent<Tilemap>();
            if(tilemap != null)
            {
                BoundsInt cellBounds = tilemap.cellBounds;

                Vector3 min = tilemap.CellToWorld(cellBounds.min);
                Vector3 max = tilemap.CellToWorld(cellBounds.max);
                Bounds tilemapBounds = new Bounds((min + max) / 2f, max - min);

                worldBounds.Encapsulate(tilemapBounds);
            }
        }
    }

    private void LateUpdate()
    {
        bool isWithinRange = (playerTransform.position.x >= transform.position.x - rangeOffset.x
            && playerTransform.position.x <= transform.position.x + rangeOffset.x
            && playerTransform.position.y >= transform.position.y - rangeOffset.y
            && playerTransform.position.y <= transform.position.y + rangeOffset.y);

        if(isWithinRange)
        {
            return;
        }

        Vector3 desiredPosition = playerTransform.position + cameraOffset;
        Vector3 targetPosition = Vector3.Lerp(transform.position, desiredPosition, cameraMoveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(targetPosition.x, worldBounds.min.x + camHalfWidth, worldBounds.max.x - camHalfWidth);
        float clampedY = Mathf.Clamp(targetPosition.y, worldBounds.min.y + camHalfHeight, worldBounds.max.y - camHalfHeight);
        targetPosition = new Vector3(clampedX, clampedY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, rangeSize);
    }
}
