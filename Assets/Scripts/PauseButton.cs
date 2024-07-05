using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUI;

    [SerializeField]
    private PlayerData playerData;

    private Button pauseButton;

    private void Awake()
    {
        pauseButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        playerData.OnPlayerDie += OnPlayerDie;
        playerData.OnPlayerUnresponsive += OnPlayerDie;
    }

    private void OnDisable()
    {
        playerData.OnPlayerDie -= OnPlayerDie;
        playerData.OnPlayerUnresponsive -= OnPlayerDie;
    }

    private void OnPlayerDie(bool value)
    {
        pauseButton.interactable = false;
    }

    public void OnClickPause()
    {
        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Click);
        pauseUI.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    public void OnClickClose()
    {
        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Click);
        pauseUI.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
