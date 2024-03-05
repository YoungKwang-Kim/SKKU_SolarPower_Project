using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DroneController : MonoBehaviour
{
    public DronCamController dronCamController;

    public GameObject myDrone;
    private Animator propAnim;
    public Transform[] waypoints; 
    public float flightSpeed;
    public Button startButton;
    public float flightHeight = 10.0f; // 비행 높이
    public float readySpeed = 2.5f; // 이착륙 속도
    public Canvas thermalCanvas;
    public Canvas minimapCanvas;

    private int waypointIndex; // waypoint들의 인덱스

    bool isDroneStart = false;

    public enum State { TakeOff, Flight, Return, Landing } //상태 설정
    public State droneState; //열거형을 담을 변수

    private void Start()
    {
        // 프로펠러 애니메이터 컴포넌트 가져오기.
        propAnim = myDrone.GetComponent<Animator>();

        // waypoint들의 y값을 드론의 flightHeight만큼 높인다.
        foreach (Transform waypoint in waypoints)
        {
            waypoint.position = new Vector3(waypoint.position.x, flightHeight, waypoint.position.z);
        }
        // 드론의 처음 상태를 이륙상태로 만든다.
        droneState = State.TakeOff;
        // 시작버튼에 OnClickButton함수를 할당한다.
        //startButton.onClick.AddListener(OnClickButton);

        dronCamController = Camera.main.GetComponent<DronCamController>();
        dronCamController.enabled = false;

        thermalCanvas.enabled = false;
        minimapCanvas.enabled = false;
    }

    private void Update()
    {
        if (isDroneStart)
        {
            SwitchDroneState();
        }
    }

    public void OnClickButton()
    {
        isDroneStart = true;
        dronCamController.enabled = true;

        thermalCanvas.enabled = true;
        minimapCanvas.enabled = true;
    }

    // 드론의 상태에 따른 행동패턴.
    public void SwitchDroneState()
    {
        //드론 상태변화
        switch (droneState)
        {
            //이륙
            case State.TakeOff:
                if (waypoints.Length == 0)
                {
                    Debug.Log("웨이포인트를 설정해주세요.");
                    break;
                }
                else if (waypoints.Length > 0)
                {
                    // 프로펠러 순서대로 회전시킨다.
                    StartPropeller();
                    // 이륙
                    myDrone.transform.Translate(Vector3.up * readySpeed * Time.deltaTime);
                    if (myDrone.transform.position.y > flightHeight)
                    {
                        droneState = State.Flight;
                    }
                }
                break;
            //비행
            case State.Flight:
                //두 거리를 비교한 뒤에 거리의 차이가 있다면 해당 waypoint로 이동
                if (Vector3.Distance(waypoints[waypointIndex].transform.position, myDrone.transform.position) > 1f)
                {
                    Move(myDrone, waypoints[waypointIndex].transform.position, flightSpeed);
                }
                //현재 waypoint에 도달한 상태라면(두 거리의 차가 0.1 이하라면)
                else
                {
                    waypointIndex++;
                    // waypoint를 다 돌았으면.
                    if (waypointIndex > waypoints.Length - 1)
                    {
                        droneState = State.Return;
                    }
                }
                break;
            // 원래 자리로 복귀
            case State.Return:

                Move(myDrone, waypoints[0].transform.position, flightSpeed);

                if (Vector3.Distance(waypoints[0].transform.position, myDrone.transform.position) < 1f)
                {
                    droneState = State.Landing;
                }

                break;
            //착륙
            case State.Landing:

                myDrone.transform.Translate(Vector3.down * readySpeed * Time.deltaTime);
                if (myDrone.transform.position.y < 5)
                {
                    readySpeed = 0;
                    PausePropeller();
                }
                break;
        }

    }
    // 타겟포인트로 이동한다.
    void Move(GameObject gameobject, Vector3 targetPoint, float speed)
    {
        // gameObject가 있는 지점에서 타겟포인트까지의 방향을 구한다.
        Vector3 relativePosition = targetPoint - gameobject.transform.position;
        relativePosition.Normalize();

        //두 지점 차이의 방향으로 normal, 그 각도 사이를 RotateTowards로 돌리기. 한 프레임에 1도씩
        gameobject.transform.rotation = Quaternion.RotateTowards(gameobject.transform.rotation, Quaternion.LookRotation(relativePosition), 2f);
        // gameObject가 목표지점을 향해 날아간다.
        gameobject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // 프로펠러를 회전시킨다.
    private void StartPropeller()
    {
        if (propAnim == null )
        {
            Debug.Log("프로펠러 애니메이션을 설정해주세요.");
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                propAnim.SetLayerWeight(i, 1);
            }
        }
    }
    // 프로펠러를 멈춘다.
    private void PausePropeller()
    {
        if (propAnim == null)
        {
            Debug.Log("프로펠러 애니메이션을 설정해주세요.");
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                propAnim.SetLayerWeight(i, 0);
            }
        }
    }

}
