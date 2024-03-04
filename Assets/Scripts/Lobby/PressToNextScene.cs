using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressToNextScene : MonoBehaviour
{
    private Material[] originalMaterials;
    private Material[] pressMaterials;

    public Material pressMaterial;

    private void Start()
    {
        // 초기 메테리얼 배열 저장
        originalMaterials = GetComponent<Renderer>().materials;

        // 하이라이트 메테리얼 추가
        pressMaterials = new Material[originalMaterials.Length + 1];
        for (int i = 0; i < originalMaterials.Length; i++)
        {
            pressMaterials[i] = originalMaterials[i];
        }

        // 기존 "HighLightShader_1" 메테리얼 추가
        pressMaterials[originalMaterials.Length] = pressMaterial;
    }

    private void OnMouseDown()
    {
        // 메테리얼 변경
        GetComponent<Renderer>().materials = pressMaterials;
    }
    private void OnMouseUp()
    {
        // 다음 씬으로 이동
        SceneManager.LoadScene(1);
    }
}