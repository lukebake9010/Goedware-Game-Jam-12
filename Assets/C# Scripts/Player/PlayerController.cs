using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : PlayerInputListener
{
    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField] 
    private Rigidbody rb;


    new void Awake()
    {
        base.Awake();
    }

    private void FixedUpdate()
    {
        // Convert Vector2 to a Vector3, mapping X to X-axis and Y to Z-axis
        Vector3 movementDirection = new Vector3(movementVector.x, 0, movementVector.y);

        // Apply force to the Rigidbody for horizontal movement
        rb.velocity = movementDirection * moveSpeed;
    }

    public override void OnAttack(InputAction.CallbackContext context)
    {

    }

    public override void OnPauseMenu(InputAction.CallbackContext context)
    {
        //Load Pause Menu
    }

    public override void OnRunePage(InputAction.CallbackContext context)
    {
        //Load Rune Page
    }

    public override void OnCycleAbility(InputAction.CallbackContext context)
    {

    }


}
