using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void RetryCurrentLevel()
    {
        audioSource.mute = !AudioManager.Instance.GetSFXState();

        if (GameManager.Instance.IsPaused())
        {
            // 플레이어를 강제로 사망상태로 만들고 충돌감지를 off 시킴
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Player>().Die();
            player.GetComponent<CrashDetector>().enabled = false;
            GameManager.Instance.ResumeGame();
        }

        LoadingSceneManager.Instance.ChangeScene(SceneManager.GetActiveScene().name);
    }
}
