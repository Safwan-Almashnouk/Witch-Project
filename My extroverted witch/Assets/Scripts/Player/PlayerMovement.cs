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
    [SerializeField]internal float speed;

    private MovementManager _movementManager;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        action = input.actions.FindAction("Movement");
        _movementManager = GetComponent<MovementManager>();

    }
   
   
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (_movementManager.CanMove == true)
        {
            Vector2 direction = action.ReadValue<Vector2>();
            transform.position += new Vector3(direction.x, 0, 0) * Time.deltaTime * speed;

        }
    }

}
   