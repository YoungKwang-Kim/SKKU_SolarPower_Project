using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimeSlider : MonoBehaviour
{
    public Slider timeSlider; 
    public Text timeText;
    private DateTime currentTime = DateTime.Now;

    private void OnEnable()
    {
        timeText.text = currentTime.ToString("HH") + ":00";
        // 슬라이더 값이 변경될 때마다
        timeSlider.onValueChanged.AddListener(UpdateTime);
        timeSlider.value = int.Parse(currentTime.ToString("HH"));
    }

    // 슬라이더 값이 변경될 때 
    void UpdateTime(float value)
    {
        // 슬라이더의 값을 시간으로 변환 -> 텍스트
        timeText.text = Mathf.RoundToInt(value).ToString() + ":00";
    }
}
