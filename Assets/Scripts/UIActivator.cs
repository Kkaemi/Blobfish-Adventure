using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject successUI;

    [SerializeField]
    private PlayerData playerData;

    private void OnEnable()
    {
        playerData.OnPlayerDie += ActiveGameOverUI;
        playerData.OnStageClear += ActiveSuccessUI;
    }

    private void OnDisable()
    {
        playerData.OnPlayerDie -= ActiveGameOverUI;
        playerData.OnStageClear -= ActiveSuccessUI;
    }

    private void ActiveGameOverUI(bool value)
    {
        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Death);
        gameOverUI.SetActive(true);
    }

    private void ActiveSuccessUI(bool value)
    {
        successUI.SetActive(true);
    }
}
