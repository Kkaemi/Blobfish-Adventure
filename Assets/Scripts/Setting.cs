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

    private void OnEnable()
    {
        musicToggle.onValueChanged.AddListener(
            (bool value) =>
            {
                AudioManager.Instance.SetMusicState(value);
                AudioManager.Instance.PlayMusic();
            }
        );
        sfxToggle.onValueChanged.AddListener(
            (bool value) =>
            {
                AudioManager.Instance.SetSFXState(value);
            }
        );
    }

    private void Start()
    {
        musicToggle.isOn = AudioManager.Instance.GetMusicState();
        sfxToggle.isOn = AudioManager.Instance.GetSFXState();
    }

    private void Update()
    {
        // 토글 버튼 누를 때 효과음 뮤트 on/off
        sfxState = !AudioManager.Instance.GetSFXState();
        musicToggle.GetComponent<AudioSource>().mute = sfxState;
        sfxToggle.GetComponent<AudioSource>().mute = sfxState;
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        bool musicState = AudioManager.Instance.GetMusicState();
        bool sfxState = AudioManager.Instance.GetSFXState();

        animator.SetTrigger("Closing");

        EncryptedPlayerPrefs.SetValue("musicState", musicState);
        EncryptedPlayerPrefs.SetValue("sfxState", sfxState);

        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
        animator.ResetTrigger("Closing");
    }
}
