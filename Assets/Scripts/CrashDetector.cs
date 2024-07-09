using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrashDetector : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!IsValidCondition(other))
        {
            return;
        }

        AudioManager.Instance.SfxPlayer.PlaySfx(SfxType.Crash);

        playerData.IsAlive = false;
    }

    private bool IsValidCondition(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Hazard":
            case "Enemy":
                break;
            default:
                return false;
        }

        if (!playerData.IsAlive || playerData.IsInvincibility || !playerData.IsResponsive)
        {
            return false;
        }

        return true;
    }
}
