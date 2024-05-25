using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrashDetector : MonoBehaviour
{
    [SerializeField]
    private AudioClip crashSound;

    [SerializeField]
    private AudioClip gameOverSound;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private Button pauseButton;

    private Player player;

    private AudioSource audioSource;

    private bool audioPlayFlag;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioPlayFlag = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player") || player.IsDead())
        {
            return;
        }

        audioSource.mute = !AudioManager.Instance.GetSFXState();
        pauseButton.interactable = false;

        if (!audioSource.isPlaying && audioPlayFlag)
        {
            audioPlayFlag = false;
            audioSource.PlayOneShot(crashSound);
            audioSource.PlayOneShot(gameOverSound);
        }

        player.Die();
        gameOverUI.SetActive(true);
    }
}
