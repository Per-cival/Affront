using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Fields\n")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;

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
        
        _inputActions.Enable();
    }

    private void FixedUpdate()
    {

    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // provider.GetState(); //BaseVector modified via ReadValue here first. If the BaseVector needs to be modified after, allow chains to do it.
        //set the GetState vector to context.ReadValue<float> and then set the velocity to that * moveSpeed?
        rb.velocity = provider.GetState().BaseVector * moveSpeed * context.ReadValue<float>();

    }
    private void OnMoveCancel(InputAction.CallbackContext context)
    {
        rb.velocity = Vector2.zero;

    }

    private void OnJump(InputAction.CallbackContext context)
    {
        rb.velocity += new Vector2(rb.velocity.x, jumpHeight);
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
