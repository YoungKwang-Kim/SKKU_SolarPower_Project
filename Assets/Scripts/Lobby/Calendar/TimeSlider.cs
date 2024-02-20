using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlider : MonoBehaviour
{
    public Slider timeSlider; // Unity Inspector 창에서 Slider를 연결합니다.
    public Text timeText; // Unity Inspector 창에서 Text를 연결합니다.

    private void Start()
    {
        // 슬라이더 값이 변경될 때마다 호출되는 이벤트를 설정합니다.
        timeSlider.onValueChanged.AddListener(UpdateTime);
    }

    // 슬라이더 값이 변경될 때 호출되는 함수
    void UpdateTime(float value)
    {
        // 슬라이더의 값을 시간 형식으로 변환하여 텍스트로 표시합니다.
        timeText.text = Mathf.RoundToInt(value).ToString() + ":00";
    }
}
