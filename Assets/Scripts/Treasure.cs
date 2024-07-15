using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.015f;

    [SerializeField]
    private float amplitude = 0.15f;

    [SerializeField]
    private PlayerData playerData;

    private Vector2 vector2;

    private void Start()
    {
        vector2 = transform.position;
    }

    private void Update()
    {
        // 사인 그래프처럼 상하 움직임
        MoveUpDownWithSin();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || !playerData.IsAlive)
        {
            return;
        }

        // 일시정지 & 쉴드 버튼 비활성화
        // 플레이어 무브먼트 0 (움직임 비활성화)
        playerData.IsResponsive = false;

        // success audio play
        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Success);

        // success ui set active
        playerData.IsSuccess = true;

        // 클리어 스테이지 카운트 증가
        GameManager.Instance.IncrementClearedStages();
    }

    private void MoveUpDownWithSin()
    {
        float y = amplitude * Mathf.Sin(Time.time * speed);
        vector2.y = y;
        transform.position = vector2;
    }
}
