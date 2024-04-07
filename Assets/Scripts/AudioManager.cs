using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    private AudioSource audioSource;

    [SerializeField]
    private bool isPlayingMusic = true;

    [SerializeField]
    private bool isPlayingSFX = true;

    private new void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (isPlayingMusic)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    public bool GetMusicState()
    {
        return isPlayingMusic;
    }

    public bool GetSFXState()
    {
        return isPlayingSFX;
    }

    public void SetMusicState(bool value)
    {
        isPlayingMusic = value;
    }

    public void SetSFXState(bool value)
    {
        isPlayingSFX = value;
    }
}
