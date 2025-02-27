using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    PlayerInput input;
    InputAction action;
    Vector2 moveDir = Vector2.zero;
    [SerializeField] float speed;
   
    internal bool canMove { get; private set; } = true;

    



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        action = input.actions.FindAction("Movement");

    }
    public void SetCanMove(bool state)
    {
        canMove = state;
    }
   
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (canMove == true)
        {
            Vector2 direction = action.ReadValue<Vector2>();
            transform.position += new Vector3(direction.x, 0, 0) * Time.deltaTime * speed;

        }
    }

}
   