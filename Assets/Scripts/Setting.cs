using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider sfxSlider;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        AudioManager.Instance.BgmPlayer.OnVolumeChange += OnMusicVolumeChane;
        AudioManager.Instance.SfxPlayer.OnVolumeChange += OnSfxVolumeChane;
    }

    private void Start()
    {
        musicSlider.value = AudioManager.Instance.BgmPlayer.BgmVolume;
        sfxSlider.value = AudioManager.Instance.SfxPlayer.SfxVolume;
    }

    private void OnDisable()
    {
        AudioManager.Instance.BgmPlayer.OnVolumeChange -= OnMusicVolumeChane;
        AudioManager.Instance.SfxPlayer.OnVolumeChange -= OnSfxVolumeChane;
    }

    private void Update()
    {
        AudioManager.Instance.BgmPlayer.BgmVolume = musicSlider.value;
        AudioManager.Instance.SfxPlayer.SfxVolume = sfxSlider.value;
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Click);

        float musicVolume = AudioManager.Instance.BgmPlayer.BgmVolume;
        float sfxVolume = AudioManager.Instance.SfxPlayer.SfxVolume;

        animator.SetTrigger("Closing");

        EncryptedPlayerPrefs.SetValue("musicVolume", musicVolume);
        EncryptedPlayerPrefs.SetValue("sfxVolume", sfxVolume);

        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
        animator.ResetTrigger("Closing");
    }

    private void OnMusicVolumeChane(float value)
    {
        musicSlider.value = value;
    }

    private void OnSfxVolumeChane(float value)
    {
        sfxSlider.value = value;
    }
}
