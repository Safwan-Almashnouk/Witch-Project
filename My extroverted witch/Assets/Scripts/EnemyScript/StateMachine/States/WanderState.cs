using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WanderState : State {

    [SerializeField] GameObject target;
    [SerializeField] float speed, sightdistance;
    [SerializeField] Vector2 targetPos;
    [SerializeField] GameObject player;

    [SerializeField] private Transform[] waypoints;
    [SerializeField] int currentTarget;

    [SerializeField] SpriteRenderer sr;
    private CapsuleCollider2D capsuleCollider;

    private bool playerFound = false;
   

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
 
    }

    public override void Enter()
    {
       
    }
    public override void Act()
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float step = speed * Time.deltaTime;

        
        targetPos.x = waypoints[currentTarget].position.x;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, step);

        
        if (Vector2.Distance(transform.position, waypoints[currentTarget].position) <= 1f)
        {
            currentTarget = (currentTarget == 0) ? 1 : 0; 
            sr.flipX = (currentTarget == 0);
        }
    }

    public override void Reason()
    {
        if (playerFound) 
        {
            GetComponent<StateMachine>().SetState(StateId.Chasing);
        }
    }

    private void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        float distanceToPlayer = direction.magnitude;

        if (distanceToPlayer <= sightdistance)
        {
            playerFound = true;
        }
    }
}
