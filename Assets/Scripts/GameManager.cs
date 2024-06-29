using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    private readonly int totalLevelCount = 20;

    private int clearStageCount = 0;

    private new void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
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
