using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseEnterInfo : MonoBehaviour
{
    public Canvas infoCanvas;

    private void Start()
    {
        StartCoroutine(InitialDelay());
    }

    private IEnumerator InitialDelay()
    {
        Time.timeScale = 0f;
        // 처음 2초 동안은 정지
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            SetCanvasAndChildrenActive(false);
        }
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