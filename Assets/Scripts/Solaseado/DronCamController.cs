using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronCamController : MonoBehaviour
{
    private Transform dronTransform = null;
    public float distance = 2f;
    public float height = 3f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    public Transform target;

    private void Start()
    {
        dronTransform = GetComponent<Transform>();
        //타겟이 없다면 Player라는 태그를 가지고 있는 게임오브젝트가 타겟이다.
        //if (target == null)
        //{
        //    target = GameObject.FindWithTag("Player").transform;
        //}
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        //카메라가 목표로 하고 있는 회전 Y축값과 높이값
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        //현재 카메라가 바라보고 있는 회전 Y축값과 높이값
        float currentRotationAngle = dronTransform.eulerAngles.y;
        float currentHeight = dronTransform.position.y;
        //현재 카메라가 바라보고 있는 회전값과 높이값을 보간해서 새로운 값으로 계산
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        //위에서 계산한 회전값으로 쿼터니언 회전값을 생성
        Quaternion currentRotation = Quaternion.Euler(0.0f, currentRotationAngle, 0.0f);
        //카메라가 타겟의 위치에서 회전하고자 하는 벡터만큼 뒤로 물러난다.
        dronTransform.position = target.position;
        dronTransform.position -= currentRotation * Vector3.forward * distance;
        //이동한 위치에서 원하는 높이값으로 올라간다.
        dronTransform.position = new Vector3(dronTransform.position.x, currentHeight, dronTransform.position.z);
        //타겟을 항상 바라보도록 한다. forward -> target
        dronTransform.LookAt(target);
    }
}
