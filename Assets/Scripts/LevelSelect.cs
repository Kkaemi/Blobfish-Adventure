using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private AudioSource audioSource;

    private TextMeshProUGUI tmp;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickLevel()
    {
        // LevelSelect.cs에서 DrawLevels 메소드가 실행되고 난 후의 변수를 사용해야 함
        // 하지만 unity 게임 오브젝트끼리의 Start 메소드 순서가 보장이 안됨
        // 그래서 해당 오브젝트를 클릭했을 때 변수를 불러오도록 구현
        tmp = GetComponentInChildren<TextMeshProUGUI>();

        audioSource.mute = !AudioManager.Instance.GetSFXState();
        audioSource.Play();

        LoadingSceneManager.Instance.ChangeScene("Scenes/Level " + tmp.text);
    }
}
