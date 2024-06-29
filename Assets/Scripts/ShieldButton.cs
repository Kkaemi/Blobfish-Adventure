using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldButton : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private GameObject shield;

    private Button shieldButton;

    private void Awake()
    {
        shieldButton = GetComponent<Button>();
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
        shieldButton.interactable = false;
    }

    public void OnShieldButtonClick()
    {
        // 중복 클릭 방지
        if (shield.activeSelf)
        {
            return;
        }

        if (playerData.ShieldCount == 0)
        {
            AdManager.Instance.ShowRewardedAd();
            return;
        }

        shield.SetActive(true);
    }
}
