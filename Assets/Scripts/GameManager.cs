using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField]
    private PlayerData playerData;

    private readonly int totalLevelCount = 20;

    private int clearStageCount = 0;

    private new void Awake()
    {
        base.Awake();

        // 프레임 60 고정
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += InitPlayerData;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= InitPlayerData;
    }

    private void InitPlayerData(Scene scene, LoadSceneMode loadSceneMode)
    {
        // 이동하는 씬이 스테이지 씬일 경우
        if (scene.name.StartsWith("Level"))
        {
            // 플레이어 데이터 초기화
            playerData.ShieldCount = EncryptedPlayerPrefs.GetValue("shieldCount", 3);
            playerData.IsSuccess = false;
            playerData.IsAlive = true;
            playerData.IsInvincibility = false;
            playerData.IsResponsive = true;
        }
    }

    public int GetTotalLevelCount()
    {
        return totalLevelCount;
    }

    public int GetClearStageCount()
    {
        return clearStageCount;
    }

    public void UpdateClearCount()
    {
        clearStageCount++;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public bool IsPaused()
    {
        return Time.timeScale == 0f;
    }
}
