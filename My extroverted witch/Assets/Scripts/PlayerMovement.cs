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
    public float Speed;
    private bool canDash;
    private float dashForce = 30f;
    private bool canMove = true;
    public Slider staminaSlider; // Reference to the UI slider
    public float stamina ; // Current stamina value
    public float staminaRegenSpeed ; // Speed of stamina regeneration
    public float maxStamina; // Maximum stamina value


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
        StaminaCheck();
       
    }

    void MovePlayer()
    {
        if(canMove == true)
        {
            Vector2 direction = action.ReadValue<Vector2>();
            transform.position += new Vector3(direction.x, 0, 0) * Time.deltaTime * Speed;
            canDash = true;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsGrounded == true)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 10f);
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
    public void Dash(InputAction.CallbackContext context)
    {

        canDash = stamina > 35 ? true : false;


        if (context.performed && canDash == true)
        {
          
            Vector2 direction = action.ReadValue<Vector2>();
            Vector3 dashVector = new Vector3(direction.x, 0, 0) * dashForce;
            rb2d.AddForce(dashVector, (ForceMode2D)ForceMode.Impulse);
            canMove = false;
            canDash = false;
            stamina = stamina - 40f;
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


    void StaminaCheck()
    {
        // Regenerate stamina if it's below the maximum
        if (stamina < maxStamina)
        {
            stamina += staminaRegenSpeed * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina); // Ensure stamina does not exceed the maximum
        }

        // Update the stamina slider
        staminaSlider.value = stamina;
    }
}
