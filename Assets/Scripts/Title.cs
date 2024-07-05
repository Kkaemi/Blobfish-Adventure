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

    private void OnEnable()
    {
        startButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Click);
        });
        optionsButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Click);
        });
    }

    public void MoveLevelSelectScene()
    {
        SceneManager.LoadScene(1);
    }
}
