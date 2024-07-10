using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBoundary : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsValidCondition(other))
        {
            return;
        }

        other.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsValidCondition(other))
        {
            return;
        }

        other.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
    }

    private bool IsValidCondition(Collider2D other)
    {
        return other.gameObject.CompareTag("Player") && playerData.IsAlive;
    }
}
