using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransitionRight : MonoBehaviour
{
    // 카메라 스크립트 변수
    private MainCameraController cam;
    // 새로운 맵 카메라 최소, 최대 범위 설정
    [SerializeField] Vector2 newMinCameraBoundary;
    [SerializeField] Vector2 newMaxCameraBoundary;
    // 플레이어가 새로운 맵으로 이동된 후 위치 조절.
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
