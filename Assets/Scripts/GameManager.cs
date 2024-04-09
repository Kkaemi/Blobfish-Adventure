using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    private readonly int totalLevelCount = 20;

    private int clearStageCount = 0;

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
