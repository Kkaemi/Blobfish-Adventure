using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WarningArea : MonoBehaviour
{
    [SerializeField]
    private float blinkInterval = 0.2f; // 깜빡임 간격

    [SerializeField]
    private int blinkTimes = 3; // 깜빡임 횟수

    [SerializeField]
    private GameObject fallingRock;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private Tween spriteTween;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        spriteTween?.Kill();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        // 효과음 재생
        audioSource.mute = !AudioManager.Instance.GetSFXState();
        audioSource.Play();

        StartCoroutine(nameof(BlinkWarningArea));
    }

    IEnumerator BlinkWarningArea()
    {
        // 위험 범위 블링크
        spriteTween = spriteRenderer.DOFade(0.7f, blinkInterval).SetLoops(-1, LoopType.Yoyo);

        yield return new WaitForSeconds(blinkInterval * 2 * blinkTimes);

        spriteTween.Kill();

        gameObject.SetActive(false);
        fallingRock.SetActive(true);
    }
}
