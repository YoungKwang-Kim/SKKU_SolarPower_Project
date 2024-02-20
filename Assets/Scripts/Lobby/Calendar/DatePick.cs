using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatePick : MonoBehaviour
{
    public void OnDateItemClick()
    {
        // 날짜 아이템을 클릭하면 해당 날짜의 텍스트를 추출하여
        // CalendarController 클래스의 특정 함수(OnDateItemClick)를 호출합니다.
        // 이렇게 함으로써 달력에서 특정 날짜를 클릭했을 때의 동작을 처리할 수 있게 됩니다.
        //CalendarController._calendarInstance.OnDateItemClick(gameObject.GetComponentInChildren<Text>().text);
        //현재 스크립트가 부착된 객체(날짜 아이템)의 자식 중에서 Text 컴포넌트를 찾아 해당 텍스트 값을 가져옵니다.
    }
}
