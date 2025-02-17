using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    private StateMachine sm;
    void Start()
    {
        sm = GetComponent<StateMachine>();
        sm.AddState(StateId.Wandering, GetComponent<WanderState>());
        sm.AddState(StateId.Chasing, GetComponent<ChaseState>());
        sm.SetState(StateId.Wandering);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
