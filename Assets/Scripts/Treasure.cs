using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.015f;

    [SerializeField]
    private float amplitude = 0.15f;

    [SerializeField]
    private GameObject successUI;

    private Vector2 vector2;

    private void Start()
    {
        vector2 = transform.position;
    }

    private void Update()
    {
        float y = amplitude * Mathf.Sin(Time.time * speed);
        vector2.y = y;
        transform.position = vector2;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.Die();
            // success audio play
            // success ui set active
            successUI.SetActive(true);
        }
    }
}
