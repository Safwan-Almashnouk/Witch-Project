using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    private SpriteRenderer Sd;
    bool facingRight;


    void Start()
    {
        Sd = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x && facingRight)
        {
            flip();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            flip();
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
