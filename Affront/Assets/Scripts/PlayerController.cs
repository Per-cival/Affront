using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Fields\n")] [SerializeField]
    private float moveSpeed;

    [SerializeField] private float jumpHeight;
    [FormerlySerializedAs("dashSpeed")] [SerializeField] private AnimationCurve dashCurve;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private int dashScalar;
    [SerializeField] private float jumpCancelFallScalar;
    [SerializeField] private float horizontalJumpScalar;
    

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
            direction = context.ReadValue<float>(); //cached to replicate 'out' parameter since these listeners cannot be overloaded 
            Vector2 result = provider.GetState().BaseVector * moveSpeed * direction;
            rb.velocity = result + new Vector2(0, rb.velocity.y);
            rb.drag = 0;
        }
    }

    private void OnMoveCancel(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.drag = 1;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (CanJump() && (int)rb.velocity.x != 0)
        {
            rb.velocity = new Vector2(moveSpeed * direction * horizontalJumpScalar, jumpHeight);
            playerState = PlayerState.Jump;
        }
        else if(CanJump() )
        {
            rb.velocity = new Vector2(0, jumpHeight);
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
        _inputActions.Default.Move.performed -= OnMove;
        _inputActions.Default.Move.canceled -= OnMoveCancel;

        isDashing = true;
        CanMove = false;
        rb.gravityScale = 0;
        provider.Zero();
        for (float i = 0; i < 5; i++) //increase i or WaitForSeconds parameter to increase dash time/distance dash will travel player
        {
            float cachedValue = dashCurve.Evaluate(i / 5);
            if (cachedValue < moveSpeed) { cachedValue = moveSpeed; } //since cachedValue decreases over time, this prevents it from dropping too low.
            
            rb.velocity = new Vector2(cachedValue * dashScalar * direction, 0f);
            yield return new WaitForSeconds(0.1f);
        }
        rb.gravityScale = 1;
        provider.Release();
        rb.velocity = new Vector2(moveSpeed * direction, 0);
        isDashing = false;
        _inputActions.Default.Move.performed += OnMove;
        _inputActions.Default.Move.canceled += OnMoveCancel;
        CanMove = true;
        if (!_inputActions.Default.Move.IsPressed())
        {
            rb.drag = 1.5f;
        }
        
    }

    private void FixedUpdate() //I avoided using Update functions as much as I could, but some things were unavoidable.
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
