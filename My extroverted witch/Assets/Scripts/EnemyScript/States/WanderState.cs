using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WanderState : State { 

    public GameObject target;
    public float speed, sightdistance, arrivaldistance;
    private Vector2 targetPos;
    [SerializeField] private Transform[] waypoints;
    private int currentTarget;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    

    public override void Enter()
    {
       
    }

    void ChooseTargetLocation()
    {
       
    }

    public override void Act()
    {
        Debug.Log(currentTarget);
        Vector3 world = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float step = speed * Time.deltaTime;
        float distance;
        
        if (currentTarget == 0)
        {
            targetPos.x = waypoints[0].position.x;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
        }
        if (currentTarget == 1)
        {
            targetPos.x = waypoints[1].position.x;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
        }
        distance = Vector2.Distance(transform.position, targetPos);
        if (Vector2.Distance(transform.position, waypoints[0].position) <= 1f)
        {
            currentTarget = 1;
            sr.flipX = false;
            
        }
        if (Vector2.Distance(transform.position, waypoints[1].position) <= 1f)
        {
            currentTarget = 0;
            sr.flipX = true;
            
        }

    }

    public override void Reason()
    {
        
    }
}
