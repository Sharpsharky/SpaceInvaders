using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class ShipInput : MonoBehaviour
{
    public float Thrust { get; private set; }

    private Vector2 analogVal;
    
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        InitializePlayerInputActions();
    }

    private void InitializePlayerInputActions()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }


    // Update is called once per frame
    void Update()
    {
        analogVal = playerInputActions.Player.Move.ReadValue<Vector2>();
        if (analogVal.x > 0.1) Thrust = 1;
        else if (analogVal.x < -0.1) Thrust = -1;
        else Thrust = 0;

        //Thrust = Input.GetAxis("Horizontal");

    }


}
