using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    
    // 날짜를 보여주는 텍스트
    public Text dateText;
    // 시간을 보여주는 텍스트
    public Text timeText;
    // 금일 발전량 데이터, 누적 발전량 데이터
    private JsonParsing powerData;

    private void Start()
    {
        powerData = new JsonParsing();
        FirstDate();
    }

    // 초기 날짜와 시간 설정
    public void FirstDate()
    {
        powerData.dateFileName = dateText.text + "_REMS";
        string[] splitTimeText = timeText.text.Split(":");
        powerData.timeFileName = splitTimeText[0] + "_50";
    }

    private void Update()
    {
        SetDataToText();
    }

    // 날짜별 시간별 데이터를 텍스트에 넣는다.
    public void SetDataToText()
    {
        // 날짜 폴더 접근
        powerData.dateFileName = dateText.text + "_REMS";
        // 시간 폴더 접근
        string[] splitTimeText = timeText.text.Split(":");
        powerData.timeFileName = splitTimeText[0] + "_50";

        powerData.StartCoroutine(GetChargeInfoCoroutine());
    }

    IEnumerator GetChargeInfoCoroutine()
    {
        yield return powerData.GetChargeInfo();
    }

}
