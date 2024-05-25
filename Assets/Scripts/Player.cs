using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movePower = 1f;

    [SerializeField]
    private float jumpPower = 1f;

    [SerializeField]
    private float maxSpeed = 3f;

    [SerializeField]
    private AudioClip jumpSound;

    private AudioSource audioSource;

    private Rigidbody2D rgbd;

    private Vector2 movement;

    private bool isAlive = true;

    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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
        if (!isAlive)
        {
            return;
        }

        Vector2 inputVector = context.ReadValue<Vector2>();

        if (Mathf.Abs(inputVector.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(inputVector.x), 1f);
        }

        movement = inputVector;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!isAlive)
        {
            return;
        }

        audioSource.mute = !AudioManager.Instance.GetSFXState();

        if (context.performed)
        {
            audioSource.PlayOneShot(jumpSound);
            rgbd.velocity += Vector2.up * jumpPower;
        }
    }

    public void Die()
    {
        isAlive = false;
        movement = Vector2.zero;
    }

    public bool IsDead()
    {
        return !isAlive;
    }
}
