using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotSpeed = 2000f;
    float mx;
    float my;
    bool isRotatable = true;

    private void Start()
    {
        // 카메라의 초기 세팅 회전값 가져오기
        mx = transform.eulerAngles.y;
        my = -transform.eulerAngles.x;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // q를 누르면 y값 하강, e를 누르면 y값 상승
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        Vector3 movement = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        transform.Translate(movement);

        // 스페이스바를 누르고 있는 동안은 마우스로 회전하지 않음
        if (!Input.GetKey(KeyCode.Space))
        {
            float rot_horizontal = Input.GetAxis("Mouse X");
            float ver_horizontal = Input.GetAxis("Mouse Y");

            mx += rot_horizontal * rotSpeed * Time.deltaTime;
            my += ver_horizontal * rotSpeed * Time.deltaTime;

            // 위아래 시야 y축 제한
            my = Mathf.Clamp(my, -80f, 80f);
            // x, y 축 회전 Vector3에 유의해서 작성
            transform.eulerAngles = new Vector3(-my, mx, 0);
        }
    }
}
