using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmPlayer : MonoBehaviour
{
    // 오디오 클립 묶음
    [Header("AudioClip")]
    [SerializeField]
    private AudioClip titleAudioClip;

    [SerializeField]
    private AudioClip stageAudioClip;

    // 오디오 소스
    private AudioSource audioSource;
    public Action<float> OnVolumeChange;
    public float BgmVolume
    {
        get { return audioSource.volume; }
        set
        {
            audioSource.volume = value;
            OnVolumeChange?.Invoke(value);
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.volume = EncryptedPlayerPrefs.GetValue("musicVolume", 0.5f);
    }

    public void PlayTitleMusic()
    {
        AudioClip beforeClip = audioSource.clip;
        if (beforeClip.Equals(titleAudioClip))
        {
            return;
        }
        audioSource.clip = titleAudioClip;
        audioSource.Play();
    }

    public void PlayStageMusic()
    {
        AudioClip beforeClip = audioSource.clip;
        if (beforeClip.Equals(stageAudioClip))
        {
            return;
        }
        audioSource.clip = stageAudioClip;
        audioSource.Play();
    }
}
