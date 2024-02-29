//using UnityEngine;
//using System.IO.Ports;


//public class ArduinoControl : MonoBehaviour
//{
//    SerialPort stream = new SerialPort("COM3", 9600); // 포트와 보드레이트 설정
//    public GameObject panel; // 로테이션을 적용할 오브젝트

//    private Quaternion prevRotation; // 이전 로테이션을 저장하는 변수

//    void Start()
//    {
//        stream.Open(); // 시리얼 포트 열기
//        prevRotation = panel.transform.rotation; // 초기 로테이션 값 저장
//    }

//    void Update()
//    {
//        if (stream.IsOpen)
//        {
//            try
//            {
//                // 서보 모터 각도를 받아오기
//                string value = stream.ReadLine();
//                float angle = float.Parse(value);

//                // 800 이하의 값에 대해서만 0도로 돌아오도록 설정
//                if (angle <= 800)
//                {
//                    panel.transform.rotation = Quaternion.Euler(0, 0, 0);
//                }
//                // 800 초과의 값에 대해서만 60도로 설정
//                else if (angle > 800 && angle != 60)
//                {
//                    panel.transform.rotation = Quaternion.Euler(0, 60, 0);
//                }

//                // 로테이션 값이 변경된 경우에만 Debug.Log를 출력
//                if (panel.transform.rotation != prevRotation)
//                {
//                    Debug.Log("현재 회전 각도: " + panel.transform.rotation.eulerAngles.y);
//                    prevRotation = panel.transform.rotation;
//                }
//            }
//            catch (System.Exception)
//            {
//                // 예외 처리 - 잘못된 형식의 데이터를 읽을 경우
//            }
//        }
//    }

//    void OnDestroy()
//    {
//        stream.Close(); // 프로그램 종료 시 포트 닫기
//    }
//}