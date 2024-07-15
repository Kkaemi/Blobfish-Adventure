using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    private Button nextLevelButton;
    private int nextLevel;

    private void Awake()
    {
        nextLevelButton = GetComponent<Button>();
    }

    private void Start()
    {
        nextLevel = GetNextLevelFromSceneName();
        SetButtonInteractable();
    }

    public void OnButtonClick()
    {
        // 버튼 클릭음 재생
        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Click);

        LoadingSceneManager.Instance.ChangeScene("Scenes/Level " + nextLevel);
    }

    // 현재 씬 이름을 기준으로 다음 스테이지 번호 얻기
    private int GetNextLevelFromSceneName()
    {
        // 스테이지 씬 이름 형식 예 : Level 1
        string currentSceneName = SceneManager.GetActiveScene().name;

        // currentSceneName.Last()은 number char이므로 '0'의 아스키 값을 빼주면 int 숫자 타입을 얻을 수 있음
        return currentSceneName.Last() - '0' + 1;
    }

    private void SetButtonInteractable()
    {
        bool isLastStage =
            GameManager.Instance.TotalLevelCount == GameManager.Instance.ClearedStages;
        nextLevelButton.interactable = !isLastStage;
    }
}
