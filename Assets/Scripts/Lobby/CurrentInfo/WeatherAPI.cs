using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherAPI : MonoBehaviour
{


    public Text temperatureText;

    void Start()
    {
        StartCoroutine(GetWeatherInfo());
    }

    IEnumerator GetWeatherInfo()
    {
        //Iat(경도) = 37.5833, Ion(위도) = 127
        //https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={API key}
        string url = $"https://api.openweathermap.org/data/2.5/weather?lat=37.5833&lon=127&appid=9de2d70f99200d51e41cdc48f150976e";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                ProcessWeatherInfo(webRequest.downloadHandler.text);
            }
        }
    }
    void ProcessWeatherInfo(string response)
    {

        WeatherData weatherData = JsonUtility.FromJson<WeatherData>(response);

        //temp(온도) 단위가kelvin
        float kelvinTemperature = weatherData.main.temp;

        // kelvin -> 섭씨
        float celsiusTemperature = kelvinTemperature - 273.15f;


        temperatureText.text = celsiusTemperature.ToString("F2") + "°C";
    }


    [System.Serializable]
    public class WeatherData
    {
        public Main main;
    }

    [System.Serializable]
    public class Main
    {
        public float temp;
    }
}