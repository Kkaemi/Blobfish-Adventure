using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
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

    private GameManager gameManager;

    private AudioManager audioManager;

    private int currentPage;
    private bool isFirstPage;
    private bool isLastPage;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        int clearStageCount = gameManager.GetClearStageCount();
        currentPage = clearStageCount / pageSize;
        DrawLevels();
    }

    private void DrawLevels()
    {
        isFirstPage = currentPage == 0;
        isLastPage = currentPage == ((gameManager.GetTotalLevelCount() / pageSize) - 1);

        leftButton.interactable = !isFirstPage;
        rightButton.interactable = !isLastPage;

        for (int i = 0; i < levelButtons.Count; i++)
        {
            TextMeshProUGUI buttonText = levelButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = (currentPage * pageSize) + i + 1 + "";
        }
    }

    public void MoveNextPage()
    {
        rightButton.GetComponent<AudioSource>().mute = !audioManager.GetSFXState();
        currentPage++;
        DrawLevels();
    }

    public void MovePreviousPage()
    {
        leftButton.GetComponent<AudioSource>().mute = !audioManager.GetSFXState();
        currentPage--;
        DrawLevels();
    }

    public void MoveMainTitleScene()
    {
        GoToMainButton.GetComponent<AudioSource>().mute = !audioManager.GetSFXState();
        SceneManager.LoadScene(0);
    }
}
