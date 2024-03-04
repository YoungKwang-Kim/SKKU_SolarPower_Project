using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Canvas exitCanvas;
    private void Start()
    {
        exitCanvas.enabled = false;
    }
    public void GoToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToPrevious()
    {
        // 현재 씬의 인덱스 가져오기
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 이전 씬의 인덱스 계산
        int previousSceneIndex = currentSceneIndex - 1;

        // 최소 인덱스 0으로 제한
        previousSceneIndex = Mathf.Clamp(previousSceneIndex, 0, SceneManager.sceneCountInBuildSettings - 1);

        // 이전 씬으로 이동
        SceneManager.LoadScene(previousSceneIndex);
    }

    public void GoToNext()
    {
        // 현재 씬의 인덱스 가져오기
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 이전 씬의 인덱스 계산
        int nextSceneIndex = currentSceneIndex + 1;

        // 최소 인덱스 0으로 제한
        nextSceneIndex = Mathf.Clamp(nextSceneIndex, 0, 2);

        // 이전 씬으로 이동
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void Exit()
    {
        exitCanvas.enabled = true;
        Time.timeScale = 0f;
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void No()
    {
        exitCanvas.enabled = false;
        Time.timeScale = 1f;
    }
}