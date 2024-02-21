using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlider : MonoBehaviour
{
    public Slider timeSlider; 
    public Text timeText;

    private void Start()
    {
        // 슬라이더 값이 변경될 때마다 호출
        timeSlider.onValueChanged.AddListener(UpdateTime);
    }

    // 슬라이더 값이 변경될 때 
    void UpdateTime(float value)
    {
        // 슬라이더의 값을 시간으로 변환 -> 텍스트
        timeText.text = Mathf.RoundToInt(value).ToString() + ":00";
    }
}
