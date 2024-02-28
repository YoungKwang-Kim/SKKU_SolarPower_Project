using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JsonParsing : MonoBehaviour
{
    // URL 구성요소
    public string dateFileName;
    public string timeFileName;
    public string[] regionFileNames = {"data_11", "data_26", "data_27", "data_28", "data_29", "data_30", "data_31", "data_36", "data_41", "data_42", "data_43", "data_44", "data_45", "data_46", "data_47", "data_48", "data_50" };
    // 발전량을 표시할 텍스트
    public TextMeshProUGUI[] powerText;
    public PowerManager powerManager;

    private void Start()
    {
        StartCoroutine(GetChargeInfo());
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

}
