using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ReloadPowerDataAndSetDataToText : MonoBehaviour
{
    // 날짜를 보여주는 텍스트
    public Text dateText;
    // 시간을 보여주는 텍스트
    public Text timeText;

    // URL 구성요소
    public string dateFileName;
    public string timeFileName;
    public string[] regionFileNames = { "data_11", "data_26", "data_27", "data_28", "data_29", "data_30", "data_31", "data_36", "data_41", "data_42", "data_43", "data_44", "data_45", "data_46", "data_47", "data_48", "data_50" };
    // 발전량을 표시할 텍스트
    public TextMeshProUGUI[] powerText;

    private void Start()
    {
        FirstDate();
    }

    public IEnumerator GetChargeInfo()
    {
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
                    PowerDataInfoArray powerDataInfoArray = JsonUtility.FromJson<PowerDataInfoArray>("{\"powerDataInfo\":" + webRequest.downloadHandler.text + "}");
                    powerText[regionFileIndex].text = powerDataInfoArray.powerDataInfo[0].dayGelec.ToString();
                    regionFileIndex++;
                }
            }
        }

    }
    // 초기 날짜와 시간 설정
    public void FirstDate()
    {
        dateFileName = dateText.text + "_REMS";
        string[] splitTimeText = timeText.text.Split(":");
        timeFileName = splitTimeText[0] + "_50";
    }
    // 날짜별 시간별 데이터를 텍스트에 넣는다.
    public void SetDataToText()
    {
        // 날짜 폴더 접근
        dateFileName = dateText.text + "_REMS";
        // 시간 폴더 접근
        string[] splitTimeText = timeText.text.Split(":");
        timeFileName = splitTimeText[0] + "_50";

        StartCoroutine(GetChargeInfoCoroutine());
    }
    IEnumerator GetChargeInfoCoroutine()
    {
        yield return GetChargeInfo();
    }
}

// 파싱할 데이터 구조
[System.Serializable]
public class PowerDataInfoArray
{
    public List<PowerData> powerDataInfo;
}

[System.Serializable]
public class PowerData
{
    public double dayGelec; // 금일발전량
    public double accumGelec; // 누적발전량
    public int co2;
    public double dayPrdct; // 금일사용량
    public double hco2;
    public double cntuAccumPowerPrdct; // 누적사용량
}

[System.Serializable]
public class InstcapaData
{
    public double gelecInstcapa;
    public double heatInstcapa;
    public double heathInstcapa;
}