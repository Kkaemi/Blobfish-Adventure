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
    private Button pauseButton;

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
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (player.IsDead())
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            player.Die();

            // change pause button status
            pauseButton.interactable = false;

            // success audio play
            audioSource.mute = !AudioManager.Instance.GetSFXState();
            audioSource.Play();

            // success ui set active
            successUI.SetActive(true);
        }
    }
}
