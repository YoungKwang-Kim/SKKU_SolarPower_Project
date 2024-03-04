using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class solCalendar : MonoBehaviour
{
    // 달력을 표시할 UI 요소들
    public GameObject calendarPanel;
    public Text yearNumText;  // 년도를 표시할 텍스트 UI
    public Text monthNumText; // 월을 표시할 텍스트 UI

    // 날짜 아이템 프리팹
    public GameObject dateitem;

   
    public List<GameObject> dateprefabs = new List<GameObject>();
    const int totalDateNum = 42; // 한 화면에 표시할 총 날짜 아이템 수

    private DateTime dateTime;
    public static solCalendar solcalendarInstance;

    void Start()
    {
        solcalendarInstance = this;

        // 날짜 아이템 초기화
        Vector3 startPos = dateitem.transform.localPosition;
        dateprefabs.Clear();
        dateprefabs.Add(dateitem);

        //날짜 아이템들을 생성하고 위치 설정
        for (int i = 1; i < totalDateNum; i++)
        {
            GameObject item = GameObject.Instantiate(dateitem) as GameObject;
            //날짜 아이템 복제하여 새로운 GameObject를 생성

            item.name = "Item" + (i + 1).ToString();
           

            item.transform.SetParent(dateitem.transform.parent);
            //생성된 GameObject를 dateitem의 부모로 설정하여 화면에 표시될 위치를 결정

            item.transform.localScale = Vector3.one;
            //생성된 GameObject의 크기를 원래 크기로 설정

            item.transform.localRotation = Quaternion.identity;
            //생성된 GameObject의 회전을 초기화
            item.transform.localPosition = new Vector3((i % 7) * 36 + startPos.x, startPos.y - (i / 7) * 30, startPos.z);
            //생성된 GameObject의 위치를 계산하여 설정. 각 날짜 아이템을 7열, 각 행 간격은 30, 열 간격은 36
            dateprefabs.Add(item);
            //생성된 GameObject를 dateprefabs 리스트에 추가합니다. 이 리스트는 후에 생성된 날짜 아이템에 접근하기 위해 사용
        }

        dateTime = DateTime.Now;

        // 현재 월의 달력 생성
        CreateCalendar();

        calendarPanel.SetActive(false); // 초기에는 달력을 보이지 않도록 설정
    }

    // 현재 월의 달력을 생성하는 함수
    void CreateCalendar()
    {
        
        DateTime firstDay = dateTime.AddDays(-(dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);
        //첫 날의 요일에 따라 해당 요일에 맞는 인덱스 값 가져옴.
        //GetDays 합수 --> 요일을 숫자로 변환

        int date = 0;
        for (int i = 0; i < totalDateNum; i++)
        {
            Text label = dateprefabs[i].GetComponentInChildren<Text>();
            dateprefabs[i].SetActive(false); //dateItem비활성화 -> 모든 날짜 초기화

            if (i >= index)
            {
                // 현재 월에 해당하는 날짜만 활성화
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    dateprefabs[i].SetActive(true); //dateItem 활성화

                    label.text = (date + 1).ToString(); //dateItem의 text안에 날짜 표시
                    date++; //다음 날짜로
                }
            }
        }
        yearNumText.text = dateTime.Year.ToString();
        monthNumText.text = dateTime.Month.ToString("D2");  // 한 자리 수 월도 두 자리로 표시 (01, 02, ..., 12)
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
        dateTime = dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext()
    {
        dateTime = dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev()
    {
        dateTime = dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext()
    {
        dateTime = dateTime.AddMonths(1);
        CreateCalendar();
    }


    // 달력을 보이게 하는 함수
    public void ShowCalendar(Text target)
    {
        calendarPanel.SetActive(true);
        _target = target;
        //calendarPanel.transform.position = new Vector3(965, 475, 0);//Input.mousePosition-new Vector3(0,120,0);
    }


    
    Text _target;
    //public Text text_Select_Date; //매개변수 없이 출력 텍스트 끌어다 놓기



    // 아이템 클릭시 Text에 날짜 표시하는 함수
    public void OnDateItemClick(string day)
    {
        _target.text = yearNumText.text + "-" + monthNumText.text + "-" + int.Parse(day).ToString("D2");
        calendarPanel.SetActive(true);
    }
}
