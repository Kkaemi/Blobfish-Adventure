using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLevelSelectButton : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    public void OnClick()
    {
        GameManager gameManager = GameManager.Instance;

        // 씬 이동 전 게임오버 또는 클리어 방지를 위해 충돌감지와 이동을 off 시킴
        playerData.IsResponsive = false;

        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Click);

        // 만약 게임이 일시정지 상태라면 강제 해제
        if (gameManager.IsPaused())
        {
            gameManager.ResumeGame();
        }

        LoadingSceneManager.Instance.ChangeScene("Scenes/Select Level");
    }
}
