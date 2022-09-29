using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Fields\n")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private AnimationCurve dashSpeed;
    [SerializeField] private int dashScalar;
    [HideInInspector] private float direction;

    private InputProvider provider;
    
    private Rigidbody2D rb;
    private PlayerInputActions _inputActions;
    private InputAction inputActionAsset;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _inputActions = new PlayerInputActions();
        
        provider = new InputProvider();
        

    }

    private void OnEnable()
    {
        _inputActions.Default.Move.started += OnMove;
        _inputActions.Default.Move.canceled += OnMoveCancel;
        _inputActions.Default.Jump.performed += OnJump;
        _inputActions.Default.Jump.canceled += OnJumpCancel;
        _inputActions.Default.Dash.started += OnDash;

        
        _inputActions.Enable();
    }

    private void FixedUpdate()
    {

    }

    private void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<float>();
        Vector2 result = provider.GetState().BaseVector * moveSpeed * context.ReadValue<float>();
        rb.velocity = result + new Vector2(0, rb.velocity.y); //include a timer scalar to run up to speed over time?

    }
    private void OnMoveCancel(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

    }

    private void OnJump(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }
    private void OnJumpCancel(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
    }
    private void OnDash(InputAction.CallbackContext context)
    {
        StartCoroutine(DashState());
    }

    IEnumerator DashState()
    {
        provider.Zero();
        for (float i = 0; i < 5; i++) //increase i to increase dash time
        {
            float cachedValue = dashSpeed.Evaluate(i / 5);
            if (cachedValue < moveSpeed)
            {
                cachedValue = moveSpeed;
            }
            rb.velocity = new Vector2(cachedValue * dashScalar * direction, 0f); 
            
            yield return new WaitForSeconds(0.1f); //decays velocity over time but drops below movespeed, then movespeed boosts so it looks weird.
        }
        provider.Release();
        rb.velocity = provider.GetState().BaseVector * direction * moveSpeed;

    }
    #region Debug Windows
    void OnGUI() //creates a debug window and injects player's velocity vector
    {
        GUI.Box(new Rect(1320, 0, 600f, 200f), "x-velocity: " + Math.Round(rb.velocity.x, 3) + 
        " y-velocity: " + Math.Round(rb.velocity.y, 3) );
        GUI.skin.box.fontSize = 25;

    }
    
    #endregion
    
}
