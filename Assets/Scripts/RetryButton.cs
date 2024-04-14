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
        LoadingSceneManager.Instance.ChangeScene(SceneManager.GetActiveScene().name);
    }
}
