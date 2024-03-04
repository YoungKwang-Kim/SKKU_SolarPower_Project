using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHoverEffect : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 targetScale;
    private Material[] originalMaterials;
    private Material[] highlightedMaterials;
    public float scaleIncrease = 1f;
    public float smoothness = 7f; // 조절 가능한 부드러움 계수
    public Material highlightMaterial; // 이미 생성되어 있는 "HighLightShader_1" 메테리얼

    void Start()
    {
        // 초기 크기 저장
        originalScale = transform.localScale;
        // 목표 크기 계산
        targetScale = originalScale;

        // 초기 메테리얼 배열 저장
        originalMaterials = GetComponent<Renderer>().materials;

        // 하이라이트 메테리얼 추가
        highlightedMaterials = new Material[originalMaterials.Length + 1];
        for (int i = 0; i < originalMaterials.Length; i++)
        {
            highlightedMaterials[i] = originalMaterials[i];
        }

        // 기존 "HighLightShader_1" 메테리얼 추가
        highlightedMaterials[originalMaterials.Length] = highlightMaterial;
    }

    void Update()
    {
        // 보간을 사용하여 부드러운 크기 변화 적용
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, smoothness * Time.deltaTime);
    }

    void OnMouseEnter()
    {
        // 마우스 오버 시 목표 크기 및 메테리얼 설정
        targetScale = originalScale + new Vector3(0, scaleIncrease, 0);
        GetComponent<Renderer>().materials = highlightedMaterials;
    }

    void OnMouseExit()
    {
        // 마우스 떠나면 초기 크기와 원래 메테리얼로 복원
        targetScale = originalScale;
        GetComponent<Renderer>().materials = originalMaterials;
    }
}