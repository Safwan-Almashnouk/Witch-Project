using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dashing : MonoBehaviour
{
    private MovementManager _movementManager;
    [SerializeField] float dashForce;
    private StaminaManager staminaManager;
    private Rigidbody2D rb2d;
    InputAction action;
    private PlayerMovement movement;
    private Vector2 moveDirection;
    PlayerInput input;

    private void Start()
    {
        staminaManager = GetComponent<StaminaManager>();
        rb2d = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        input = GetComponent<PlayerInput>();
        action = input.actions.FindAction("Movement");
        _movementManager = GetComponent<MovementManager>();
    }

    public void Dash(InputAction.CallbackContext context)
    {

        _movementManager.CanDash = staminaManager.stamina > 35 ? true : false;

        if (context.performed && _movementManager.CanDash == true)
        {

            Vector2 direction = action.ReadValue<Vector2>();
            Vector3 dashVector = new Vector3(direction.x, 0, 0) * dashForce;
            rb2d.velocity = new Vector2(direction.x * dashForce, rb2d.velocity.y);
            _movementManager.CanMove = false;
            _movementManager.CanDash = false;
            staminaManager.UseStamina();
            StartCoroutine(WaitForDash());

        }

    }
    IEnumerator WaitForDash()
    {
        yield return new WaitForSeconds(0.4f);
        rb2d.velocity = Vector3.zero;
        _movementManager.CanMove = true;
        _movementManager.CanDash = true;
    }
}
