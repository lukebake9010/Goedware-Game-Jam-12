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
    [SerializeField]
    private Camera cam;

    new void Awake()
    {
        base.Awake();

        PlayerCameraBeacon camBeacon = PlayerCameraBeacon.Instance;
        if (camBeacon == null) return;
        cam = camBeacon.GetCamera();
    }

    public override void Update()
    {
        base.Update();

        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert mouse screen position to world position
        Ray ray = cam.ScreenPointToRay(mouseScreenPosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Assuming the ground is on the XZ plane
        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(rayDistance);

            // Calculate direction to the mouse
            Vector3 direction = mouseWorldPosition - transform.position;
            direction.y = 0; // Ignore Y for top-down rotation

            // Calculate the rotation angle (Y-axis rotation)
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Apply the rotation to the mage
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
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
        PlayerManager playerManager = PlayerManager.Instance;
        if (playerManager == null) return;
        playerManager.PlayerCombatAttack();
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
