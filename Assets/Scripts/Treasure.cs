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
    private GameObject successUI;

    [SerializeField]
    private PlayerData playerData;

    private AudioSource audioSource;
    private Vector2 vector2;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        vector2 = transform.position;
    }

    private void Update()
    {
        float y = amplitude * Mathf.Sin(Time.time * speed);
        vector2.y = y;
        transform.position = vector2;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || !playerData.IsAlive)
        {
            return;
        }

        // 일시정지, 쉴드 버튼 비활성화 시켜야됨
        // 플레이어 무브먼트 0 만들어야 됨
        playerData.IsResponsive = false;

        // success audio play
        audioSource.mute = !AudioManager.Instance.GetSFXState();
        audioSource.Play();

        // success ui set active
        successUI.SetActive(true);
    }
}
