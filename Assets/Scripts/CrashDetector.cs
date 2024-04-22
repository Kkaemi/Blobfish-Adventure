using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    [SerializeField]
    private AudioClip crashSound;

    [SerializeField]
    private AudioClip gameOverSound;

    [SerializeField]
    private GameObject gameOverUI;

    private AudioSource audioSource;

    private Player player;

    private bool audioPlayFlag;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponent<Player>();
        audioPlayFlag = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.mute = !AudioManager.Instance.GetSFXState();
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
}
