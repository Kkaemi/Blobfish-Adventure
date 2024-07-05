using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    public void RetryCurrentLevel()
    {
        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Click);

        if (GameManager.Instance.IsPaused())
        {
            // 씬 이동 전 게임오버 또는 클리어 방지를 위해 충돌감지와 이동을 off 시킴
            playerData.IsResponsive = false;
            GameManager.Instance.ResumeGame();
        }

        LoadingSceneManager.Instance.ChangeScene(SceneManager.GetActiveScene().name);
    }
}
