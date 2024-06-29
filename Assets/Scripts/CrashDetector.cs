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
    private PlayerData playerData;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        playerData.OnPlayerDie += ActiveGameOverUI;
    }

    private void OnDisable()
    {
        playerData.OnPlayerDie -= ActiveGameOverUI;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!IsValidCondition(other))
        {
            return;
        }

        audioSource.mute = !AudioManager.Instance.GetSFXState();

        audioSource.PlayOneShot(crashSound);
        audioSource.PlayOneShot(gameOverSound);

        playerData.IsAlive = false;
    }

    private bool IsValidCondition(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Hazard":
            case "Enemy":
                break;
            default:
                return false;
        }

        if (!playerData.IsAlive || playerData.IsInvincibility || !playerData.IsResponsive)
        {
            return false;
        }

        return true;
    }

    private void ActiveGameOverUI(bool value)
    {
        gameOverUI.SetActive(true);
    }
}
