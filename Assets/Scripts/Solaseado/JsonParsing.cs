using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherAPI : MonoBehaviour
{
    [SerializeField] Text text;
    void Start()
    {
        StartCoroutine(GetChargeInfo());
    }

    IEnumerator GetChargeInfo()
    {
        string url = $"https://rems.energy.or.kr/admin/monitor/monitorCmb/gelecHeatData?cityProvCode=46&rgnCode=10&userType=&bsmanId=";

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

    private void ProcessChargeInfo(PowerData powerData)
    {
        text.text = $"금일 발전량: {powerData.dayGelec}";
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
    public double dayGelec;
    public double accumGelec;
    public int co2;
    public double dayPrdct;
    public double hco2;
    public double cntuAccumPowerPrdct;
}

[System.Serializable]
public class InstcapaData
{
    public double gelecInstcapa;
    public double heatInstcapa;
    public double heathInstcapa;
}