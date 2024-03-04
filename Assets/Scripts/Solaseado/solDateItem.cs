using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class solDateItem : MonoBehaviour
{
  public void OnDateItemClick()
    {
        solCalendar.solcalendarInstance.OnDateItemClick(gameObject.GetComponentInChildren<Text>().text);
    }
   
}
