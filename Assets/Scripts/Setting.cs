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

    private bool sfxState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        musicToggle.isOn = AudioManager.Instance.GetMusicState();
        sfxToggle.isOn = AudioManager.Instance.GetSFXState();
    }

    private void Update()
    {
        sfxState = !AudioManager.Instance.GetSFXState();
        musicToggle.GetComponent<AudioSource>().mute = sfxState;
        sfxToggle.GetComponent<AudioSource>().mute = sfxState;

        musicToggle.onValueChanged.AddListener(
            (bool value) =>
            {
                AudioManager.Instance.PlayMusic();
                AudioManager.Instance.SetMusicState(value);
            }
        );
        sfxToggle.onValueChanged.AddListener(
            (bool value) =>
            {
                AudioManager.Instance.SetSFXState(value);
            }
        );
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
