using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LoadingSceneManager : SingletonBehaviour<LoadingSceneManager>
{
    [SerializeField]
    private CanvasGroup fadeImage;

    [SerializeField]
    private float fadeDuration = 2f;

    [SerializeField]
    private GameObject LoadingUI;

    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ChangeScene(string sceneName)
    {
        fadeImage
            .DOFade(1, fadeDuration)
            .OnStart(() =>
            {
                fadeImage.blocksRaycasts = true;
            })
            .OnComplete(() =>
            {
                StartCoroutine(nameof(LoadScene), sceneName);
            });
    }

    private IEnumerator LoadScene(string sceneName)
    {
        LoadingUI.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        float pastTime = 0f;
        float percentage = 0f;

        while (!async.isDone)
        {
            yield return null;

            pastTime += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, pastTime);

                if (percentage == 100)
                {
                    async.allowSceneActivation = true;
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, pastTime);
                if (percentage >= 90)
                    pastTime = 0;
            }
            textMeshPro.text = percentage.ToString("0") + "%";
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeImage
            .DOFade(0f, fadeDuration)
            .OnStart(() =>
            {
                LoadingUI.SetActive(false);
            })
            .OnComplete(() =>
            {
                fadeImage.blocksRaycasts = false;
            });
    }
}
