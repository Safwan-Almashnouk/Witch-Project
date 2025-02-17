using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WanderState : State {

    [SerializeField] GameObject target;
    [SerializeField] float speed, sightdistance;
    [SerializeField] Vector2 targetPos;

    [SerializeField] private Transform[] waypoints;
    [SerializeField] int currentTarget;

    [SerializeField] SpriteRenderer sr;
    [SerializeField]Transform playerPos;
   

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
 
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
        float distance = Vector2.Distance(gameObject.transform.position, playerPos.position);
        if (distance <= sightdistance)
        { 
            GetComponent<StateMachine>().SetState(StateId.Chasing);
        }

    }
}
