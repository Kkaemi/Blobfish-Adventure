using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUI;

    [SerializeField]
    private PlayerData playerData;

    private Button pauseButton;
    private AudioSource audioSource;

    private void Awake()
    {
        pauseButton = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
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
        CheckMute();
        pauseUI.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    public void OnClickClose()
    {
        CheckMute();
        pauseUI.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    private void CheckMute()
    {
        audioSource.mute = !AudioManager.Instance.GetSFXState();
        audioSource.Play();
    }
}
