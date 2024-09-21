using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputListener : MonoBehaviour
{
    UserInputActions inputActions;

    public void Awake()
    {
        inputActions = new UserInputActions();

        inputActions.Player.PauseMenu.performed += OnPauseMenu;
        inputActions.Player.RunePage.performed += OnRunePage;

        inputActions.Player.CycleAbility.performed += OnCycleAbility;

        inputActions.Enable();
    }

    [SerializeField]
    protected Vector2 movementVector = Vector2.zero;

    public virtual void Update()
    {
        movementVector = inputActions.Player.Movement.ReadValue<Vector2>();
    }

    public virtual void OnPauseMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Pause Menu");
    }

    public virtual void OnRunePage(InputAction.CallbackContext context)
    {

    }

    public virtual void OnCycleAbility(InputAction.CallbackContext context)
    {

    }
}
