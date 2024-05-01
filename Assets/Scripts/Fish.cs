using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private float movePower = 1f;

    [SerializeField]
    private float jumpPower = 1.5f;

    [SerializeField]
    private float raycastDistance = 1f;

    private Rigidbody2D rgbd;

    private CapsuleCollider2D capsuleCollider2D;

    private int moveDirection;

    [SerializeField]
    private float maxSpeed = 10f;

    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        StartCoroutine(nameof(Think));
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    IEnumerator Think()
    {
        WaitForSeconds delay = new(3f);

        while (true)
        {
            moveDirection = Random.Range(-1, 2);
            if (moveDirection != 0)
            {
                transform.localScale = new Vector3(-moveDirection, 1, 1);
            }
            yield return delay;
        }
    }

    private void Move()
    {
        rgbd.AddForce(new Vector2(moveDirection, 0) * movePower);

        if (rgbd.velocity.magnitude > maxSpeed)
        {
            rgbd.velocity = rgbd.velocity.normalized * maxSpeed;
        }
    }

    private void Jump()
    {
        Vector2 rayPosition =
            new(capsuleCollider2D.bounds.center.x, capsuleCollider2D.bounds.min.y);

        Physics2D.queriesHitTriggers = false;

        RaycastHit2D rcht = Physics2D.Raycast(
            rayPosition,
            Vector2.down,
            raycastDistance,
            LayerMask.GetMask("Background")
        );

        if (rcht.collider != null)
        {
            StartCoroutine(nameof(JumpWithDelay));
            Debug.DrawRay(rayPosition, Vector2.down * raycastDistance, Color.red);
        }
        else
        {
            Debug.DrawRay(rayPosition, Vector2.down * raycastDistance, Color.green);
        }
    }

    IEnumerator JumpWithDelay()
    {
        WaitForSeconds delay = new(1f);
        rgbd.velocity += Vector2.up * jumpPower;
        yield return delay;
        rgbd.velocity += Vector2.up * (jumpPower / 2);
    }
}
