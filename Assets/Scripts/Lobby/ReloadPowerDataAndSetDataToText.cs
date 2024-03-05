using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Security.Policy;
using XCharts.Runtime;

public class ReloadPowerDataAndSetDataToText : MonoBehaviour
{
    // 날짜를 보여주는 텍스트
    public Text dateText;
    // 시간을 보여주는 텍스트
    public Text timeText;
    // 광역시 탑3 텍스트 지역 이름
    public TextMeshProUGUI[] topThreeTextName;
    // 광역시 탑3 텍스트 데이터
    public TextMeshProUGUI[] topThreeText;
    // Pie 그래프
    public PieChart pieChart;
    // 도넛그래프 탑4 텍스트
    public TextMeshProUGUI[] topFourText;
    // 실시간 전국 총 생산량
    public double total = 0;

    // URL 구성요소
    public string dateFileName;
    public string timeFileName;
    private string[] DashboardRegionNames = { "data_11", "data_26", "data_27", "data_28", "data_29", "data_30", "data_31", "data_41", "data_42", "data_43", "data_44", "data_45", "data_46", "data_47", "data_48" };
    string[] RegionKoreaName = { "서울특별시", "부산광역시", "대구광역시", "인천광역시", "광주광역시", "대전광역시", "울산광역시", "경기도", "강원도", "충청북도", "충청남도", "전라북도", "전라남도", "경상북도", "경상남도" };
    private Dictionary<string, double> topThreeRegionData = new Dictionary<string, double>();
    private Dictionary<string, double> topFourRegionData = new Dictionary<string, double>();

    // 발전량을 표시할 텍스트
    public TextMeshProUGUI[] powerText;


    private void Start()
    {
        SetDataToText();
    }

    public IEnumerator GetChargeInfo()
    {
        // 지역파일의 인덱스
        int regionFileIndex = 0;
        // 실시간 전국 총 생산량
        total = 0;

        foreach (string regionFileName in DashboardRegionNames)
        {
            // 요청할 url
            string url = $"https://solarpowerdata-default-rtdb.firebaseio.com/{dateFileName}/{timeFileName}/{regionFileName}.json";

            // 웹에 요청을 보낸다.
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log($"Error : {webRequest.error}");
                }
                else
                {
                    // 가져온 JSON파일을 파싱한다.
                    PowerDataInfoArray powerDataInfoArray = JsonUtility.FromJson<PowerDataInfoArray>("{\"powerDataInfo\":" + webRequest.downloadHandler.text + "}");
                    total += powerDataInfoArray.powerDataInfo[0].dayGelec;
                    powerText[regionFileIndex].text = powerDataInfoArray.powerDataInfo[0].dayGelec.ToString();
                    regionFileIndex++;
                }
            }
        }
        DescendingTopThreeDictionary(topThreeRegionData);
        DescendingTopFourDictionary(topFourRegionData);
        topThreeRegionData.Clear();
        topFourRegionData.Clear();
    }
    // 광역시 탑3 딕셔너리 내림차순으로 만들기
    void DescendingTopThreeDictionary(Dictionary<string, double> dict)
    {
        // 딕셔너리에 값 추가
        for (int i = 0; i < 7; i++)
        {
            dict.Add(RegionKoreaName[i], double.Parse(powerText[i].text));
        }
        // 딕셔너리를 내림차순으로 정렬
        var sortedDictionary = dict.OrderByDescending(kvp => kvp.Value);
        // 인덱스번호
        int index = 0;
        // 1, 2, 3위 텍스트에 표시
        foreach (var kvp in sortedDictionary)
        {
            topThreeTextName[index].text = $"{kvp.Key}";
            topThreeText[index].text = $"{kvp.Value}MWh";
            index++;
            if (index == 3)
                break;
        }
        dict.Clear();
    }
    // 도별 탑4 딕셔너리 내림차순 만들기
    void DescendingTopFourDictionary(Dictionary<string, double> dict)
    {
        // 딕셔너리에 값 추가
        for (int i = 7; i < 15; i++)
        {
            dict.Add(RegionKoreaName[i], double.Parse(powerText[i].text));
        }
        // 딕셔너리를 내림차순으로 정렬
        var sortedDictionary = dict.OrderByDescending(kvp => kvp.Value);
        // 인덱스번호
        int index = 0;
        // 1 ~ 4위 PieChart에 표시한다.
        foreach (var kvp in sortedDictionary)
        {
            double value = 360 * kvp.Value / total;
            pieChart.UpdateData(index, 0, value);
            pieChart.UpdateDataName(index, 0, kvp.Key);
            pieChart.UpdateDataName(index, 1, kvp.Key);
            topFourText[index].text = (value * 100 / 360 ).ToString("F1") + "%";
            index++;
            if (index == 4)
                break;
        }
    }
    // 초기 날짜와 시간 설정
    public void SetText()
    {
        // 날짜 폴더 접근
        dateFileName = dateText.text + "_REMS";
        // 시간 폴더 접근
        string[] splitTimeText = timeText.text.Split(":");
        // 한시간 전 데이터를 불러온다.
        int splitTime = int.Parse(splitTimeText[0]) - 1;
        // 시간이 10시 이전이라면
        if (splitTime < 10)
        {
            timeFileName = "0" + splitTime.ToString() + "_50";
        }
        // 
        else if (splitTime < 0)
        {
            timeFileName = "00_50";
        }
        else
        {
            timeFileName = splitTime + "_50";
        }
    }
    // 날짜별 시간별 데이터를 텍스트에 넣는다.
    public void SetDataToText()
    {
        // 날짜와 시간 폴더 접근
        SetText();
        StartCoroutine(GetChargeInfo());
    }
    
}