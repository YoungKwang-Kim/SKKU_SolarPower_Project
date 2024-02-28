using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JsonParsing : MonoBehaviour
{
    // [SerializeField] Text text;
    // URL 구성요소
    [SerializeField ]private string dateFileName = "2024-02-19_REMS";
    public string timeFileName = "16_50";
    public string regionFileName = "data_11";

    void Start()
    {
        StartCoroutine(GetChargeInfo());
    }

    IEnumerator GetChargeInfo()
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
                ProcessChargeInfo(powerDataInfoArray.powerDataInfo[0]);
            }
        }
    }
    // 발전량 데이터를 텍스트로 보여준다.
    private void ProcessChargeInfo(PowerData powerData)
    {
        // text.text = $"금일 발전량: {powerData.dayGelec}";
        Debug.Log($"금일 발전량: {powerData.dayGelec}");
        Debug.Log($"누적 발전량: {powerData.accumGelec}");
        Debug.Log($"금일 사용량: {powerData.dayPrdct}");
        Debug.Log($"누적 사용량: {powerData.cntuAccumPowerPrdct}");
    }
}

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