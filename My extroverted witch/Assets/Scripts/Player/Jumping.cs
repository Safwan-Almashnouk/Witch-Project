using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : MonoBehaviour
{

    [SerializeField] bool isGrounded;
    private Rigidbody2D rb2d;
    [SerializeField] float jumpForce;
    private MovementManager _movementManager;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        _movementManager = GetComponent<MovementManager>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && _movementManager.CanJump == true)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
    }

    private void SetGroundedState(Collision2D other, bool state)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = state;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) => SetGroundedState(other, true);
    private void OnCollisionExit2D(Collision2D other) => SetGroundedState(other, false);
}
