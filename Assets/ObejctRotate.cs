using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ObejctRotate : MonoBehaviour
{
    // 태양의 고도를 움직일 오브젝트 (부모) (x축 회전)
    public Transform testAltitude;
    // 태양의 방위각을 움직일 오브젝트 (y축 회전)
    public Transform testAzimuth;
    // 태양의 고도를 API를 통해 불러와서 변수에 저장하기

    // 태양의 고도 회전하기
    public void RotateAltitude(float value)
    {
        testAltitude.rotation = Quaternion.Euler(new Vector3(value, 0, 0));
    }
    // 태양의 방위각 회전하기
    public void RotateAzimuth(float value)
    {
        testAzimuth.localRotation = Quaternion.AngleAxis(value, Vector3.up);
    }

    private void Start()
    {
        RotateAltitude(20);
        RotateAzimuth(30);

    }
    private void Update()
    {
        
    }



}
