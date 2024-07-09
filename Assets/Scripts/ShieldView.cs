using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShieldView : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        UpdateShieldCount(playerData.ShieldCount);
    }

    private void OnEnable()
    {
        playerData.OnShieldCountChange += UpdateShieldCount;
    }

    private void OnDisable()
    {
        playerData.OnShieldCountChange -= UpdateShieldCount;
    }

    public void UpdateShieldCount(int shieldCount)
    {
        textMeshProUGUI.text = shieldCount == 0 ? "AD" : (shieldCount + "");
    }
}
