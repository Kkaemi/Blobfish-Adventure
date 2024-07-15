using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPage : MonoBehaviour
{
    [SerializeField]
    private Button leftButton;

    [SerializeField]
    private Button rightButton;

    [SerializeField]
    private List<Button> levelButtons = new();

    [SerializeField]
    private Button GoToMainButton;

    private readonly int pageSize = 10;

    // 0부터 시작
    private int currentPage;

    private void Start()
    {
        GetCurrentPage();
        SetPageMoveButtonState();
        DrawLevels();
    }

    // 10단위로만 버튼 세팅
    // 예) 총 스테이지 수 가 18일 경우
    // 총 페이지 수 1로 설정 1 ~ 10까지만 노출 11 ~ 18은 보이지 않고 페이지도 이동 안됨
    private void DrawLevels()
    {
        int clearStage = GameManager.Instance.ClearedStages;

        for (int i = 0; i < levelButtons.Count; i++)
        {
            Button levelButton = levelButtons[i];
            int currentLevel = (currentPage * pageSize) + i + 1;

            bool isCleared = currentLevel <= clearStage;
            bool isLockedLevel = currentLevel > clearStage + 1;

            levelButton.GetComponent<Image>().color = isCleared ? Color.green : Color.white;
            levelButton.interactable = !isLockedLevel;

            TextMeshProUGUI buttonText = levelButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentLevel.ToString();
        }
    }

    // 플레이하던 스테이지 페이지부터 보여줌
    // 예) 전에 11스테이지를 클리어 했다면 1페이지부터 보여줌
    private void GetCurrentPage()
    {
        int maxPage = GameManager.Instance.TotalLevelCount / pageSize - 1;
        currentPage = GameManager.Instance.ClearedStages / pageSize;

        if (currentPage > maxPage)
        {
            currentPage--;
        }
    }

    private void SetPageMoveButtonState()
    {
        bool isFirstPage = currentPage == 0;
        bool isLastPage = currentPage == ((GameManager.Instance.TotalLevelCount / pageSize) - 1);

        leftButton.interactable = !isFirstPage;
        rightButton.interactable = !isLastPage;
    }

    public void MoveNextPage()
    {
        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Hover);
        currentPage++;
        SetPageMoveButtonState();
        DrawLevels();
    }

    public void MovePreviousPage()
    {
        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Hover);
        currentPage--;
        SetPageMoveButtonState();
        DrawLevels();
    }

    public void MoveMainTitleScene()
    {
        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Click);
        SceneManager.LoadScene(0);
    }
}
