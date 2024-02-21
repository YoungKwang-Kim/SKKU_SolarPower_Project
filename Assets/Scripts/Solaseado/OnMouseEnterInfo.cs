using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEnterTest : MonoBehaviour
{
    public Canvas infoCanvas;
    private void Start()
    {
        infoCanvas.enabled = false;
    }
    private void OnMouseEnter()
    {
        infoCanvas.enabled = true;
        Debug.Log("마우스 호버");
    }

    private void OnMouseExit()
    {
        infoCanvas.enabled = false;
    }
}