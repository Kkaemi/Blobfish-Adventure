using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLevelSelectButton : MonoBehaviour
{
    private AudioSource audioSource;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        GameManager gameManager = GameManager.Instance;

        audioSource.mute = !AudioManager.Instance.GetSFXState();
        audioSource.Play();

        // 만약 게임이 일시정지 상태라면 강제 해제
        if (gameManager.IsPaused())
        {
            gameManager.ResumeGame();
        }

        // 플레이어를 강제로 비활성화 시켜서 씬 이동중 게임오버 or 클리어 방지
        if (!player.IsDead())
        {
            player.Die();
        }

        LoadingSceneManager.Instance.ChangeScene("Scenes/Level Select");
    }
}
