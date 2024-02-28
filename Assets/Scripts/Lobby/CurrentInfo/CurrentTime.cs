using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTime : MonoBehaviour
{
    public Text currentDate; 
    public Text currentTime;
    public DateTime currentDateTime = DateTime.Now;

    void OnEnable()
    {
        UpdateCurrent();

        InvokeRepeating("UpdateCurrent", 1f, repeatRate:1f); 
    }

    // Update is called once per frame
    void UpdateCurrent()
    {
        currentDate.text = currentDateTime.ToString("yyyy³â MM¿ù ddÀÏ");
        currentTime.text = currentDateTime.ToString("HH:mm");
    }
}
