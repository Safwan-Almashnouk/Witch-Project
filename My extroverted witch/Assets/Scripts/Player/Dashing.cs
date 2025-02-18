using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dashing : MonoBehaviour
{
    private bool canDash;
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
    }

    public void Dash(InputAction.CallbackContext context)
    {

        canDash = staminaManager.stamina > 35 ? true : false;

        if (context.performed && canDash == true)
        {

            Vector2 direction = action.ReadValue<Vector2>();
            Vector3 dashVector = new Vector3(direction.x, 0, 0) * dashForce;
            rb2d.velocity = new Vector2(direction.x * dashForce, rb2d.velocity.y);
            movement.SetCanMove(false);
            canDash = false;
            staminaManager.UseStamina();
            StartCoroutine(WaitForDash());

        }

    }
    IEnumerator WaitForDash()
    {
        yield return new WaitForSeconds(0.4f);
        rb2d.velocity = Vector3.zero;
        movement.SetCanMove(true);
        canDash = true;
    }
}
