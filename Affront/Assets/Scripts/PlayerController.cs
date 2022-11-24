using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Fields\n")] [SerializeField]
    private float moveSpeed;

    [SerializeField] private float jumpHeight;
    [SerializeField] private AnimationCurve dashSpeed;
    [SerializeField] private int dashScalar;
    [SerializeField] private float jumpCancelFallScalar;

    private static bool CanMove = true;

    private bool isDashing;
    [SerializeField] private float dashTimer;

    private InputProvider provider;
    private float direction;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private PlayerInputActions _inputActions;
    private InputAction inputActionAsset;
    private PlayerState playerState;

    private GameObject pauseHandler;
    private MenuHandler handler;

    private enum PlayerState
    {
        Neutral,
        Jump,
        Injured,
        Attack
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        _inputActions = new PlayerInputActions();

        provider = new InputProvider();

        playerState = PlayerState.Neutral;

        pauseHandler = new GameObject("Pause", typeof(MenuHandler));
        handler = pauseHandler.GetComponent<MenuHandler>();
    }

    private void OnEnable()
    {
        _inputActions.Default.Move.performed += OnMove;
        _inputActions.Default.Move.canceled += OnMoveCancel;
        _inputActions.Default.Jump.started += OnJump;
        _inputActions.Default.Jump.canceled += OnJumpCancel;
        _inputActions.Default.Dash.started += OnDash;
        _inputActions.Default.Pause.started += OnPause;
        _inputActions.Default.Attack.started += OnAttack;

        _inputActions.Default.FullscreenToggle.started += OnFullscreenToggle;

        _inputActions.Enable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (!isDashing && CanMove)
        {
            direction = context.ReadValue<float>();
            Vector2 result = provider.GetState().BaseVector * moveSpeed * context.ReadValue<float>();
            rb.velocity = result + new Vector2(0, rb.velocity.y); 
        }
    }

    private void OnMoveCancel(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (CanJump())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            playerState = PlayerState.Jump;
        } 
    }
    private bool CanJump()
    {
        LayerMask mask = LayerMask.GetMask("Platform");
        float distance = 0.3f;
        RaycastHit2D hit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f , Vector2.down, distance, mask);
        return hit.collider != null;
    }

    private void OnJumpCancel(InputAction.CallbackContext context)
    {
        if (rb.velocity.y > 7.5)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
    }
    private void OnDash(InputAction.CallbackContext context)
    {
        if (playerState == PlayerState.Jump)
        {
            StartCoroutine(DashState());
        }
        playerState = PlayerState.Neutral;
    }
    private void OnPause(InputAction.CallbackContext context)
    {
        handler.OnPause();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        //attack
    }

    private void OnFullscreenToggle(InputAction.CallbackContext context)
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    IEnumerator DashState()
    {
        isDashing = true;
        CanMove = false;
        rb.gravityScale = 0;
        provider.Zero();
        for (float i = 0; i < 5; i++) //increase i or WaitForSeconds parameter to increase dash time
        {
            float cachedValue = dashSpeed.Evaluate(i / 5);
            if (cachedValue < moveSpeed) { cachedValue = moveSpeed; } //since cachedValue decreases over time, this prevents it from dropping too low.
            
            rb.velocity = new Vector2(cachedValue * dashScalar * direction, 0f);
            yield return new WaitForSeconds(0.1f);
        }
        rb.gravityScale = 1;
        provider.Release();
        rb.velocity = new Vector2(moveSpeed * direction, 0);
        isDashing = false;
        CanMove = true;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 1.5f);
        }
    }

    #region Debug Windows

    void OnGUI()
    {
        GUI.Box(new Rect(1320, 0, 600f, 200f), "x-velocity: " + Math.Round(rb.velocity.x, 3) + "\ny-velocity: " + Math.Round(rb.velocity.y, 3));
        GUI.skin.box.fontSize = 25;
    }

    #endregion
    
}
