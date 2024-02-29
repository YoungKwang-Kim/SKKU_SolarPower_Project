using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseEnterTest : MonoBehaviour
{
    public Canvas infoCanvas;

    private void Start()
    {
        SetCanvasAndChildrenActive(false);
    }

    private void OnMouseEnter()
    {
        SetCanvasAndChildrenActive(true);
        Debug.Log("마우스 호버");
    }

    private void OnMouseExit()
    {
        SetCanvasAndChildrenActive(false);
    }

    private void SetCanvasAndChildrenActive(bool active)
    {
        infoCanvas.enabled = active;

        // 자식 오브젝트들에 대한 활성화/비활성화 처리
        foreach (Transform child in infoCanvas.transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}