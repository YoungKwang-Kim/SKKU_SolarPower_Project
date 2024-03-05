using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermalCamSync : MonoBehaviour
{
    private Transform mainCameraTransform;
    public Transform droneCamera;

    private void Start()
    {
        // 메인 카메라의 Transform을 찾습니다.
        mainCameraTransform = droneCamera.transform;

        // 만약 메인 카메라가 없다면 경고를 표시합니다.
        if (mainCameraTransform == null)
        {
            Debug.LogWarning("Main Camera not found!");
        }
    }

    private void Update()
    {
        // 메인 카메라의 position과 rotation 값을 현재 카메라에 적용합니다.
        if (mainCameraTransform != null)
        {
            transform.position = mainCameraTransform.position;
            transform.rotation = Quaternion.Euler(30, mainCameraTransform.rotation.y, 0);
        }
    }
}