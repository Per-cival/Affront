using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class MurdochAI : MonoBehaviour
{
    [Header("Murdoch Stats")] 
    [SerializeField] private uint health;
    [SerializeField] private int damage;
    [SerializeField] private int MoveSpeed;
    [SerializeField] private int knockbackX;
    [SerializeField] private int knockbackY; //when the player hits this entity, the knockback applied to it in each direction
    
    

    
    private BaseState state;
    private AttackTypes attackTypes; //attack enum specific to each enemy, therefore no global enum
    

    private Rigidbody2D rb;
    private Transform ai;
    private GameObject player;
    private BoxCollider2D bc;
    private BoxCollider2D rangeTrigger;
    private int playerLeftOfBoss; //storing direction as integer instead of bool to directly manipulate velocity vector.
    private bool followPlayer;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        rangeTrigger = gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        ai = GetComponent<Transform>(); //caching to wrap name of variable
        player = FindObjectOfType<PlayerController>().gameObject; //generally bad, however PlayerController is a confirmed unique type
        state = BaseState.Wandering;
    }

    private enum AttackTypes
    {
        Normal,
        Heavy,
        Special
    }

    private void Update()
    {
        switch(state) //considering switching to FSM, but implementation is simple and no plans for extension
        {
            case BaseState.Wandering :
                Wander();
                break;
            case BaseState.Discovered:
                Discover();
                break;
            case BaseState.Attacking:
                Attack();
                break;
        }
        
        RaycastHit2D hitLeft = Physics2D.Raycast(bc.bounds.center, Vector2.left, 5f);
        RaycastHit2D hitRight = Physics2D.Raycast(bc.bounds.center, Vector2.right, 5f);
        Debug.DrawRay(bc.bounds.center, Vector2.left * 5);
        Debug.DrawRay(bc.bounds.center, Vector2.right * 5);
    }

    private void Wander()
    {
        rb.velocity = Vector2.zero;
    }

    private void Discover()
    {
        Move();
        //Move towards player. When close enough (via ray), switch to attack 
        //cache reference to player. Fire OnTriggerEnter when inside bounding box (faster than registering a ray collision each frame)
    }

    
    private void Attack()
    {
        rb.velocity = Vector2.zero;
        //pick randomly between 3 separate attacks. Attack, pause for a short duration, and then either dash backward, jump toward player, or attack again. 
        //weight attacks so it's predetermined? like after attack 2, he always jumps at you. etc.
    }

    private void OnTriggerEnter2D(Collider2D col) //uses trigger collider rather than hitbox collider, collision with OnCollisionEnter
    {
        if (col.CompareTag("Player"))
        {
            state = BaseState.Discovered;
            followPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //state = BaseState.Wandering;
        if (other.CompareTag("Player"))
        {
            followPlayer = false;
            playerLeftOfBoss = 0; //remove this when I implement a real wander. This is placeholder
        }
    }

    private void OnCollisionEnter2D(Collision2D col) //this works but I need to steal movement first
    {
        if (playerLeftOfBoss == -1) //if player is left of boss then
        {
            col.rigidbody.AddForce(new Vector2(1 - (Mathf.Cos(135 * Mathf.Deg2Rad) + knockbackX), Mathf.Sin(135 * Mathf.Deg2Rad) + knockbackY), ForceMode2D.Impulse); 
        }
        else
        {
            col.rigidbody.AddForce(new Vector2(Mathf.Cos(45 * Mathf.Deg2Rad) + knockbackX, Mathf.Sin(45 * Mathf.Deg2Rad) + knockbackY), ForceMode2D.Impulse);

        }
        //this solution causes problems when the player is still moving. I need a static reference to the movement vector. Does that 100% mean singleton solution?
        //the vector would be passed as a separate chain? Like I have an "injured" listener solved in a manager instead of here.
        
    }

    private int FindPosition()
    {
        if (!followPlayer) return 0;
        if ((int)player.transform.position.x + 5 < (int)ai.position.x) //if player's x-position is less than AI, then player is left of AI, and vice versa.
        {
            playerLeftOfBoss = -1;
        }
        else if ((int)player.transform.position.x - 5> (int)ai.position.x) //casting to int to avoid float imprecision.
        {
            playerLeftOfBoss = 1;
        }
        else playerLeftOfBoss = 0;

        return playerLeftOfBoss;
    }
    private void Move() => rb.velocity = new Vector2(FindPosition() * MoveSpeed, 0);

    
}
