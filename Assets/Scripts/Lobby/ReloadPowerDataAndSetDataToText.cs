using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class ReloadPowerDataAndSetDataToText : MonoBehaviour
{
    // 날짜를 보여주는 텍스트
    public Text dateText;
    // 시간을 보여주는 텍스트
    public Text timeText;
    // URL 구성요소
    public string dateFileName;
    public string timeFileName;
    private string[] regionFileNames = { "data_11", "data_26", "data_27", "data_28", "data_29", "data_30", "data_31", "data_36", "data_41", "data_42", "data_43", "data_44", "data_45", "data_46", "data_47", "data_48", "data_50" };
    // 도시번호와 발전량을 담는 딕셔너리
    Dictionary<string, double> CityAndChargeData;
    // 발전량을 표시할 텍스트
    public TextMeshProUGUI[] powerText;

    private void Start()
    {
        // SetText();
    }

    public IEnumerator GetChargeInfo()
    {
        // 지역파일의 인덱스
        int regionFileIndex = 0;

        foreach (string regionFileName in regionFileNames)
        {
            // 요청할 url
            string url = $"https://solarpowerdata-default-rtdb.firebaseio.com/{dateFileName}/{timeFileName}/{regionFileName}.json";
            
            // 웹에 요청을 보낸다.
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log($"Error : {webRequest.error}");
                }
                else
                {
                    // 가져온 JSON파일을 파싱한다.
                    PowerDataInfoArray powerDataInfoArray = JsonUtility.FromJson<PowerDataInfoArray>("{\"powerDataInfo\":" + webRequest.downloadHandler.text + "}");
                    powerText[regionFileIndex].text = powerDataInfoArray.powerDataInfo[0].dayGelec.ToString();
                    regionFileIndex++;
                }
            }
        }
    }
    // 초기 날짜와 시간 설정
    public void SetText()
    {
        // 날짜 폴더 접근
        dateFileName = dateText.text + "_REMS";
        // 시간 폴더 접근
        string[] splitTimeText = timeText.text.Split(":");
        // 시간이 10시 이전이라면
        if (int.Parse(splitTimeText[0]) < 10) timeFileName = "0" + splitTimeText[0] + "_50";
        else timeFileName = splitTimeText[0] + "_50";
    }
    // 날짜별 시간별 데이터를 텍스트에 넣는다.
    public void SetDataToText()
    {
        // 날짜와 시간 폴더 접근
        SetText();

        StartCoroutine(GetChargeInfoCoroutine());
    }
    // 발전량 데이터를 다 가져오면 데이터를 텍스트에 넣는 코루틴입니다.
    IEnumerator GetChargeInfoCoroutine()
    {
        yield return GetChargeInfo();
    }
}