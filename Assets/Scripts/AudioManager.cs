using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonBehaviour<AudioManager>
{
    [SerializeField]
    private GameObject bgmPlayer;
    public BgmPlayer BgmPlayer => bgmPlayer.GetComponent<BgmPlayer>();

    [SerializeField]
    private GameObject sfxPlayer;
    public SfxPlayer SfxPlayer => sfxPlayer.GetComponent<SfxPlayer>();

    private new void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ChangeBGMByScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ChangeBGMByScene;
    }

    public void ChangeBGMByScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        bool isLevelScene = scene.name.StartsWith("Level");

        if (isLevelScene)
        {
            BgmPlayer.PlayStageMusic();
        }
        else
        {
            BgmPlayer.PlayTitleMusic();
        }
    }
}
