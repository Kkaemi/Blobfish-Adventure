using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldButton : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    private Button shieldButton;

    private void Awake()
    {
        shieldButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        playerData.OnPlayerDie += OnPlayerDie;
        playerData.OnPlayerUnresponsive += OnPlayerDie;
        playerData.OnPlayerInvincibilityToggle += OnPlayerInvincibilityToggle;
    }

    private void OnDisable()
    {
        playerData.OnPlayerDie -= OnPlayerDie;
        playerData.OnPlayerUnresponsive -= OnPlayerDie;
        playerData.OnPlayerInvincibilityToggle -= OnPlayerInvincibilityToggle;
    }

    private void OnPlayerDie(bool value)
    {
        shieldButton.interactable = false;
    }

    private void OnPlayerInvincibilityToggle(bool value)
    {
        shieldButton.interactable = !value;
    }

    public void OnShieldButtonClick()
    {
        if (playerData.ShieldCount == 0)
        {
            AdManager.Instance.ShowRewardedAd();
            return;
        }

        playerData.IsInvincibility = true;
    }
}
