using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioController : MonoBehaviour
{
    public float time = 15f;
    public Image sectorDStatus;
    public GameObject droneButton;

    private void Start()
    {
        droneButton.SetActive(false);
        StartCoroutine(ChangeColorAndSpawnButton());
    }

    IEnumerator ChangeColorAndSpawnButton()
    {
        yield return new WaitForSeconds(time);

        // 이미지의 색 변경
        if (sectorDStatus != null)
        {
            sectorDStatus.color = Color.red;
        }

        droneButton.SetActive(true);
    }
}