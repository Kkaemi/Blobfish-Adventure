using UnityEngine;

public class Pause : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private GameObject pauseUI;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
