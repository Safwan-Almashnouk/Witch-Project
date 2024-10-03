using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool IsGrounded = false;
    PlayerInput input;
    InputAction action;
    Vector2 moveDir =  Vector2.zero;
    public float Speed;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        action = input.actions.FindAction("Movement");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 direction = action.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, 0) * Time.deltaTime * Speed;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsGrounded == true)
            {
                rb2d.AddForce(Vector3.up * 5f, ForceMode2D.Impulse);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }
}
