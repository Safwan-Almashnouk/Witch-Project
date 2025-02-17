using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    [SerializeField] float Speed;
    [SerializeField] GameObject playerPos;
    [SerializeField] float jumpforce;
    [SerializeField] float stopDistance;
    [SerializeField] float lastPosX;
    [SerializeField] SpriteRenderer sr;

    [SerializeField] GameObject player;


    internal bool canAttack;

    public override void Enter()
    {
        lastPosX = transform.position.x;
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

    }
    public override void Act()
    {
        Vector2 direction = playerPos.transform.position - transform.position;
        float distanceToPlayer = direction.magnitude;

        
        if (distanceToPlayer >= stopDistance)
        {
            Vector2 normalizedDirection = direction.normalized;
            transform.position = Vector2.MoveTowards(transform.position, playerPos.transform.position, Speed * Time.deltaTime);
            canAttack = true;
            
        }
    }

    private void FixedUpdate()
    {
      

        
        if (player.transform.position.x > transform.position.x)
        {
            sr.flipX = true; 
        }
        else
        {
            sr.flipX = false; 
        }
    }
    public override void Reason()
    {
        
    }
}
