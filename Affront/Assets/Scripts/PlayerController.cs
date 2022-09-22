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
    
    
    private Rigidbody2D rb;
    private PlayerInputActions _inputActions;
    private InputAction inputActionAsset;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Default.Jump.performed += OnJump;
        
        _inputActions.Enable();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), 0);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
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
