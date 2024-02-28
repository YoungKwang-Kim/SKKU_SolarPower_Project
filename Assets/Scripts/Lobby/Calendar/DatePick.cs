using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatePick : MonoBehaviour
{
    public Slider slider;
    public void OnDateItemClick()
    {
        CalendarController._calendarInstance.OnDateItemClick(gameObject.GetComponentInChildren<Text>().text);
        //현재 스크립트가 부착된 객체(날짜 아이템)의 자식 중에서 Text 컴포넌트의 텍스트 가져오기
        slider.value = 12f;
    }
}
