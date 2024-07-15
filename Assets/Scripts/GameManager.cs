using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField]
    private PlayerData playerData;

    // 10단위로만 설정해야 함
    // LevelPage.cs에 보충설명 있음
    private readonly int totalLevelCount = 20;
    public int TotalLevelCount
    {
        get => totalLevelCount;
    }

    private int clearedStages;
    public int ClearedStages
    {
        get => clearedStages;
    }

    private new void Awake()
    {
        base.Awake();

        // 프레임 60 고정
        Application.targetFrameRate = 60;

        // 파일에서 클리어 스테이지 개수 불러옴, 디폴트 0
        clearedStages = EncryptedPlayerPrefs.GetValue("clearedStages", 0);
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
            playerData.ShieldCount = EncryptedPlayerPrefs.GetValue("shieldCount", 0);
            playerData.IsSuccess = false;
            playerData.IsAlive = true;
            playerData.IsInvincibility = false;
            playerData.IsResponsive = true;
        }
    }

    public void IncrementClearedStages()
    {
        clearedStages++;
        EncryptedPlayerPrefs.SetValue("clearedStages", clearedStages);
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
