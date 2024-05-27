using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips;

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
        SceneManager.sceneLoaded += ChangeBGMByScene;
    }

    private void OnEnable()
    {
        PlayMusic();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= ChangeBGMByScene;
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

    public void ChangeBGMByScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        AudioClip beforeClip = audioSource.clip;
        bool isLevelScene = scene.name.StartsWith("Level");

        if (isLevelScene)
        {
            audioSource.clip = audioClips[1];
        }
        else
        {
            audioSource.clip = audioClips[0];
        }

        if (beforeClip.Equals(audioSource.clip))
        {
            return;
        }

        PlayMusic();
    }
}
