using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    [SerializeField] float Speed;
    [SerializeField] GameObject player;
    [SerializeField] float jumpforce;
    [SerializeField] float stopDistance;
    [SerializeField] float lastPosX;
    [SerializeField] SpriteRenderer sr;

 


    internal bool canAttack;

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public override void Enter()
    {
        lastPosX = transform.position.x;
       
        

    }
    public override void Act()
    {
        Vector2 direction = player.transform.position - transform.position;
        float distanceToPlayer = direction.magnitude;

        
        if (distanceToPlayer >= stopDistance)
        {
            Vector2 normalizedDirection = direction.normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
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
