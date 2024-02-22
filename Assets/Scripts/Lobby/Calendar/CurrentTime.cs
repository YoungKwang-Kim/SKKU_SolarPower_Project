using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTime : MonoBehaviour
{
    public Text currentDate; 
    public Text currentTime;

    void Start()
    {
        UpdateCurrent();

        InvokeRepeating("UpdateCurrent", 1f, repeatRate:1f); 
    }

    // Update is called once per frame
    void UpdateCurrent()
    {
        DateTime currentDateTime = DateTime.Now;

        currentDate.text = currentDateTime.ToString("yyyy³â MM¿ù ddÀÏ");
        currentTime.text = currentDateTime.ToString("HH:mm");
    }
}
