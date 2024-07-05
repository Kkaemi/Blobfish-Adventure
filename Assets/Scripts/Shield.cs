using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    private SpriteRenderer spriteRenderer;
    private Tween shieldTween;

    private readonly float shieldDuration = 6f; // 쉴드 지속시간(초)
    private readonly float blinkDuration = 3f; // 블링크 지속시간(초)
    private readonly float blinkInterval = 0.5f; // 블링크 간격(초)

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(DeployShield));
    }

    private void OnDestroy()
    {
        shieldTween?.Kill();
    }

    IEnumerator DeployShield()
    {
        playerData.ShieldCount -= 1;

        // 플레이어 무적상태 전환
        playerData.IsInvincibility = true;

        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Shield);

        // spriteRenderer의 원래 알파값
        float originalAlpha = spriteRenderer.color.a;

        yield return new WaitForSeconds(shieldDuration - blinkDuration);

        // 블링크
        shieldTween = spriteRenderer.DOFade(0f, blinkInterval).SetLoops(-1, LoopType.Yoyo);

        yield return new WaitForSeconds(blinkDuration);

        shieldTween.Kill();
        spriteRenderer.DOFade(originalAlpha, 0f);

        gameObject.SetActive(false);

        playerData.IsInvincibility = false;
    }
}
