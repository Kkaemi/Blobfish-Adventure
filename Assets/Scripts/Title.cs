using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button optionsButton;

    private AudioManager audioManager;
    private bool sfxState;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        sfxState = !audioManager.GetSFXState();
        startButton.GetComponent<AudioSource>().mute = sfxState;
        optionsButton.GetComponent<AudioSource>().mute = sfxState;
    }

    public void MoveLevelSelectScene()
    {
        SceneManager.LoadScene(1);
    }
}
