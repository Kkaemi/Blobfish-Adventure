using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/PlayerData")]
public class PlayerData : ScriptableObject
{
    // 플레이어 생존 여부 플래그 값
    private bool isAlive;
    public Action<bool> OnPlayerDie;
    public bool IsAlive
    {
        get { return isAlive; }
        set
        {
            if (!value)
            {
                OnPlayerDie?.Invoke(value);
            }
            isAlive = value;
        }
    }

    // 플레이어 무적 상태 플래그 값
    private bool isInvincibility;
    public Action<bool> OnPlayerInvincibilityToggle;
    public bool IsInvincibility
    {
        get { return isInvincibility; }
        set
        {
            if (isInvincibility == value)
            {
                return;
            }
            isInvincibility = value;
            OnPlayerInvincibilityToggle?.Invoke(value);
        }
    }

    // 클리어 또는 씬 이동시 충돌 무시 && 움직임 방지 플래그 값
    private bool isResponsive;
    public Action<bool> OnPlayerUnresponsive;
    public bool IsResponsive
    {
        get { return isResponsive; }
        set
        {
            if (!value)
            {
                OnPlayerUnresponsive?.Invoke(value);
            }
            isResponsive = value;
        }
    }

    // 쉴드 카운트
    private int shieldCount;
    public Action<int> OnShieldCountChange;
    public int ShieldCount
    {
        get { return shieldCount; }
        set
        {
            if (shieldCount == value)
            {
                return;
            }
            shieldCount = value;
            OnShieldCountChange?.Invoke(value);
            EncryptedPlayerPrefs.SetValue("shieldCount", value);
        }
    }

    // 현재 스테이지 클리어 여부
    private bool isSuccess;
    public Action<bool> OnStageClear;
    public bool IsSuccess
    {
        get => isSuccess;
        set
        {
            if (value)
            {
                OnStageClear?.Invoke(value);
            }
            isSuccess = value;
        }
    }
}
