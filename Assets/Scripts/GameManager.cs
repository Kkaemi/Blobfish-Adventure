using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly int totalLevelCount = 20;

    private int clearStageCount = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
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
}
