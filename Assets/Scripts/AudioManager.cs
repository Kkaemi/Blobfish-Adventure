using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    private bool isPlayingMusic = true;
    private bool isPlayingSFX = true;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMusic();
    }

    void PlayMusic()
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

    public void ToggleMusicState()
    {
        isPlayingMusic = !isPlayingMusic;
        PlayMusic();
    }

    public void ToggleSFXState()
    {
        isPlayingSFX = !isPlayingSFX;
    }

    public bool GetMusicState()
    {
        return isPlayingMusic;
    }

    public bool GetSFXState()
    {
        return isPlayingSFX;
    }
}
