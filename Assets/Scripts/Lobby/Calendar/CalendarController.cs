using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    // 달력을 표시할 UI 요소들
    public GameObject _calendarPanel;
    public Text _yearNumText;  // 년도를 표시할 텍스트 UI
    public Text _monthNumText; // 월을 표시할 텍스트 UI

    // 달력 날짜 아이템 프리팹
    public GameObject _item;

    // 날짜 아이템들의 리스트
    public List<GameObject> _dateItems = new List<GameObject>();
    const int _totalDateNum = 42; // 한 화면에 표시할 총 날짜 아이템 수

    private DateTime _dateTime;
    public static CalendarController _calendarInstance;

    void Start()
    {
        _calendarInstance = this;

        // 날짜 아이템 초기화
        Vector3 startPos = _item.transform.localPosition;
        _dateItems.Clear();
        _dateItems.Add(_item);

        //달력 각 날짜 아이템들을 생성하고 위치 설정
        for (int i = 1; i < _totalDateNum; i++)
        {
            GameObject item = GameObject.Instantiate(_item) as GameObject;
            //날짜 아이템 프리팹 _item을 복제하여 새로운 GameObject를 생성

            item.name = "Item" + (i + 1).ToString();
            //: 생성된 GameObject의 이름을 설정

            item.transform.SetParent(_item.transform.parent);
            //생성된 GameObject를 _item의 부모로 설정하여 화면에 표시될 위치를 결정

            item.transform.localScale = Vector3.one;
            //생성된 GameObject의 크기를 원래 크기로 설정

            item.transform.localRotation = Quaternion.identity;
            //생성된 GameObject의 회전을 초기화
            item.transform.localPosition = new Vector3((i % 7) * 36 + startPos.x, startPos.y - (i / 7) * 30, startPos.z);
            //생성된 GameObject의 위치를 계산하여 설정. 각 날짜 아이템을 7열로 나열하고, 각 행 간격은 30, 열 간격은 36
            _dateItems.Add(item);
            //생성된 GameObject를 _dateItems 리스트에 추가합니다. 이 리스트는 후에 생성된 날짜 아이템에 접근하기 위해 사용
        }

        _dateTime = DateTime.Now;

        // 현재 월의 달력 생성
        CreateCalendar();

        _calendarPanel.SetActive(false); // 초기에는 달력을 보이지 않도록 설정
    }

    // 현재 월의 달력을 생성하는 함수
    void CreateCalendar()
    {
        // 현재 월의 첫 날 계산
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);
        //첫 날의 요일에 따라 해당 요일에 맞는 인덱스 값 가져옴.
        //GetDays 합수 --> 요일을 숫자로 변환

        int date = 0;
        for (int i = 0; i < _totalDateNum; i++)
        {
            Text label = _dateItems[i].GetComponentInChildren<Text>();
            _dateItems[i].SetActive(false); //dateItem비활성화 -> 모든 날짜 초기화

            if (i >= index)
            {
                // 현재 월에 해당하는 날짜만 활성화
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    _dateItems[i].SetActive(true); //dateItem 활성화

                    label.text = (date + 1).ToString(); //dateItem의 text안에 날짜 표시
                    date++; //다음 날짜로
                }
            }
        }
        _yearNumText.text = _dateTime.Year.ToString();
        _monthNumText.text = _dateTime.Month.ToString("D2");  // 한 자리 수 월도 두 자리로 표시 (01, 02, ..., 12)
    }

    // 요일에 따른 인덱스 반환
    int GetDays(DayOfWeek day) //GetDays 함수 -> 요일을 숫자로 변환하는 함수
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 1;
            case DayOfWeek.Tuesday: return 2;
            case DayOfWeek.Wednesday: return 3;
            case DayOfWeek.Thursday: return 4;
            case DayOfWeek.Friday: return 5;
            case DayOfWeek.Saturday: return 6;
            case DayOfWeek.Sunday: return 0;
        }

        return 0;
    }


    public void YearPrev()
    {
        _dateTime = _dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext()
    {
        _dateTime = _dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev()
    {
        _dateTime = _dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext()
    {
        _dateTime = _dateTime.AddMonths(1);
        CreateCalendar();
    }

    // 달력을 보이게 하는 함수
    public void ShowCalendar(Text target)
    {
        _calendarPanel.SetActive(true);
        _target = target;
        //_calendarPanel.transform.position = new Vector3(965, 475, 0);//Input.mousePosition-new Vector3(0,120,0);
    }

    Text _target;

    // 아이템 클릭시 Text에 날짜 표시하는 함수
    public void OnDateItemClick(string day)
    {
        _target.text = _yearNumText.text + "-" + _monthNumText.text + "-" + int.Parse(day).ToString("D2");
        //_calendarPanel.SetActive(true);
    }
}
