using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField]
    private Toggle musicToggle;

    [SerializeField]
    private Toggle sfxToggle;

    private Animator animator;

    private AudioManager audioManager;

    private bool sfxState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        musicToggle.isOn = audioManager.GetMusicState();
        sfxToggle.isOn = audioManager.GetSFXState();
    }

    private void Update()
    {
        sfxState = !audioManager.GetSFXState();
        musicToggle.GetComponent<AudioSource>().mute = sfxState;
        sfxToggle.GetComponent<AudioSource>().mute = sfxState;
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("Closing");
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        animator.ResetTrigger("Closing");
    }
}
