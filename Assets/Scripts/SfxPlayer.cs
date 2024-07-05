using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum SfxType
{
    Click,
    Crash,
    Jump,
    Death,
    Shield,
    Hazard,
    Success
}

[Serializable]
public class SfxClip
{
    [SerializeField]
    private SfxType type;
    public SfxType Type => type;

    [SerializeField]
    private AudioClip clip;
    public AudioClip Clip => clip;
}

public class SfxPlayer : MonoBehaviour
{
    // 오디오 클립 묶음
    [SerializeField]
    private List<SfxClip> sfxClipList;
    private Dictionary<SfxType, AudioClip> sfxClipDictionary;

    // 오디오 소스
    private AudioSource audioSource;
    public Action<float> OnVolumeChange;
    public float SfxVolume
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
        sfxClipDictionary = sfxClipList.ToDictionary(
            sfxClip => sfxClip.Type,
            sfxClip => sfxClip.Clip
        );
    }

    private void Start()
    {
        audioSource.volume = EncryptedPlayerPrefs.GetValue<float>("sfxVolume");
    }

    public void PlaySfx(SfxType sfxType)
    {
        if (sfxClipDictionary.TryGetValue(sfxType, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SfxClip of type {sfxType} not found!");
        }
    }
}
