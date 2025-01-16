using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    public float moveSpeed;
    private int currentTarget;
    private SpriteRenderer sr;
    private bool isPatrolling = true;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling == true)
        {
            if (currentTarget == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[0].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, waypoints[0].position) < 0.2f)
                {
                    currentTarget = 1;
                    sr.flipX = true;
                }
            }
            if (currentTarget == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[1].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, waypoints[1].position) < 0.2f)
                {
                    currentTarget = 0;
                    sr.flipX = false;
                }
            }   
        }

       
    }
}
