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
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;

        // 카메라가 살짝 움직이거나 아무 키나 누르면 캔버스 비활성화
        yield return StartCoroutine(WaitForCameraMovementOrAnyKey());
        SetCanvasAndChildrenActive(false);
    }

    private IEnumerator WaitForCameraMovementOrAnyKey()
    {
        Vector3 initialCameraPosition = Camera.main.transform.position;

        // 대기 시간 동안 카메라가 약간 움직이는 것을 체크
        Vector3 targetCameraPosition = new Vector3(initialCameraPosition.x + 0.1f, initialCameraPosition.y, initialCameraPosition.z);
        float elapsedTime = 0f;

        while (elapsedTime < 0.1f)
        {
            Camera.main.transform.position = Vector3.Lerp(initialCameraPosition, targetCameraPosition, elapsedTime / 0.1f);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // 아무 키 입력이 있을 때까지 대기
        while (!Input.anyKeyDown)
        {
            yield return null;
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