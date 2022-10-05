using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurdochAI : MonoBehaviour
{
    [Header("Murdoch Stats")] 
    [SerializeField] private int health;
    [SerializeField] private int damage;
    private BaseState state;

    private void Update()
    {
        switch(state)
        {
            case BaseState.Discovered : //logic
                break;
           
        }
    }
}
