using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool IsGrounded = false;
    PlayerInput input;
    InputAction action;
    Vector2 moveDir =  Vector2.zero;

    [SerializeField] float speed;
    private bool canDash;
    [SerializeField] float dashForce;
    private bool canMove = true;
    [SerializeField] float jumpForce;
    private Vector2 moveDirection;

    private StaminaManager staminaManager;
  
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        action = input.actions.FindAction("Movement");
        staminaManager = GetComponent<StaminaManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if(canMove)
        {
            Vector2 direction = action.ReadValue<Vector2>();
            transform.position += new Vector3(direction.x, 0, 0) * Time.deltaTime * speed;
            canDash = true;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded == true)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
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
    public void Dash(InputAction.CallbackContext context)
    {

        canDash = staminaManager.stamina > 35 ? true : false;

        if (context.performed && canDash == true)
        {
          
            Vector2 direction = action.ReadValue<Vector2>();
            Vector3 dashVector = new Vector3(direction.x, 0, 0) * dashForce;
            rb2d.velocity = new Vector2(direction.x * dashForce, rb2d.velocity.y);
            canMove = false;
            canDash = false;
            staminaManager.UseStamina();
            StartCoroutine(WaitForDash());

        }

    }
    IEnumerator WaitForDash()
    {
        yield return new WaitForSeconds(0.4f);
        rb2d.velocity = Vector3.zero;
        canMove = true;
        canDash = true;
    }

}
