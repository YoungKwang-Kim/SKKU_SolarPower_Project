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
    public DateTime currentDateTime;

    void Start()
    {
        InvokeRepeating("UpdateCurrent", 1f, 1f);
    }

    // Update is called once per frame
    void UpdateCurrent()
    {
        currentDateTime = DateTime.Now;
        currentDate.text = currentDateTime.ToString("yyyy ³â  MM ¿ù  dd ÀÏ");
        currentTime.text = currentDateTime.ToString("HH:mm");
    }
}