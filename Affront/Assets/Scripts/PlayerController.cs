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
    [Header("Movement Fields\n")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private AnimationCurve dashSpeed;
    [SerializeField] private int dashScalar;
    [SerializeField] private float xVelocityJumpScalar;
    [SerializeField] private float jumpCancelFallScalar;
    
    
    private bool isDashing;
    [SerializeField]private float dashTimer;
    
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

        #if UNITY_STANDALONE
        Cursor.visible = false; //small settings like these should realistically go in a manager class, but I opted out of one because the scope is small.
        #endif

    }

    private void OnEnable()
    {
        _inputActions.Default.Move.started += OnMove;
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
        if (!isDashing)
        {
            direction = context.ReadValue<float>();
            Vector2 result = provider.GetState().BaseVector * moveSpeed * context.ReadValue<float>();
            rb.velocity = result + new Vector2(0, rb.velocity.y); //include a timer scalar to run up to speed over time?
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
        float distance = 0.5f;
        RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(bc.bounds.min.x, bc.bounds.min.y), Vector2.down, distance, mask);
        RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(bc.bounds.max.x, bc.bounds.min.y), Vector2.down, distance, mask);
        //could've used a boxcast, will refactor if the need arises
        

        if (leftHit.collider != null && rightHit.collider != null)
        {
            return true; 
        }

        return false;
    }
    private void OnJumpCancel(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / jumpCancelFallScalar);
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
        //rb.velocity = provider.GetState().BaseVector * direction * moveSpeed;
        rb.velocity = Vector2.zero;
        isDashing = false;

    }
    #region Debug Windows
    void OnGUI() //creates a debug window and injects player's velocity vector. OnGUI is called roughly once a frame. 
    {
        GUI.Box(new Rect(1320, 0, 600f, 200f), "x-velocity: " + Math.Round(rb.velocity.x, 3) + 
        " y-velocity: " + Math.Round(rb.velocity.y, 3) );
        GUI.skin.box.fontSize = 25;

    }
    
    #endregion
    
}
