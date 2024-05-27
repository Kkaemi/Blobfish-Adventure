using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movePower = 7f;

    [SerializeField]
    private float jumpPower = 30f;

    [SerializeField]
    private float maxSpeed = 6f;

    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private PlayerData playerData;

    private AudioSource audioSource;

    private Rigidbody2D rgbd;

    private Vector2 movement;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        playerData.OnPlayerDie += Die;
        playerData.OnPlayerUnresponsive += Die;
        playerData.OnPlayerInvincibilityToggle += OnToggleInvincibility;
    }

    private void Start()
    {
        playerData.IsAlive = true;
        playerData.IsInvincibility = false;
        playerData.IsResponsive = true;
    }

    private void OnDisable()
    {
        playerData.OnPlayerDie -= Die;
        playerData.OnPlayerUnresponsive -= Die;
        playerData.OnPlayerInvincibilityToggle -= OnToggleInvincibility;
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
        if (!CanMove())
        {
            return;
        }

        Vector2 inputVector = context.ReadValue<Vector2>();
        FlipSprite(inputVector);
        movement = inputVector;
    }

    private void FlipSprite(Vector2 inputVector)
    {
        if (Mathf.Abs(inputVector.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(inputVector.x), 1f);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!CanMove())
        {
            return;
        }

        if (context.performed)
        {
            audioSource.mute = !AudioManager.Instance.GetSFXState();
            audioSource.PlayOneShot(jumpSound);
            rgbd.velocity = Vector2.up * jumpPower;
        }
    }

    private bool CanMove()
    {
        if (!playerData.IsAlive || !playerData.IsResponsive)
        {
            return false;
        }

        return true;
    }

    private void Die(bool value)
    {
        // 가속도를 유지한채 죽으면 계속 바닥에서 미끄러지는 현상 방지
        movement = Vector2.zero;
    }

    private void OnToggleInvincibility(bool value)
    {
        // 무적상태일 때 적이나 장애물과 부딪히면 플레이어가 기울지 않도록 막아줌
        rgbd.freezeRotation = !rgbd.freezeRotation;
    }
}
