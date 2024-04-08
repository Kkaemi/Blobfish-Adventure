using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movePower = 1f;

    [SerializeField]
    private float jumpPower = 1f;

    [SerializeField]
    private float maxSpeed = 3f;

    private Rigidbody2D rgbd;

    private Vector2 movement;

    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rgbd.AddForce(movePower * movement);

        if (rgbd.velocity.magnitude > maxSpeed)
        {
            rgbd.velocity = rgbd.velocity.normalized * maxSpeed;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        if (Mathf.Abs(inputVector.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(inputVector.x), 1f);
        }

        movement = inputVector;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rgbd.velocity += Vector2.up * jumpPower;
        }
    }
}
