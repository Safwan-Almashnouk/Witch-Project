using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<StateId, State> states = new Dictionary<StateId, State>();
    private State currentState;

    private void Update()
    {
        if (currentState == null)
            return;

        currentState.Reason();
        currentState.Act();
    }

    public void SetState(StateId stateID)
    {
        if(!states.ContainsKey(stateID))
            return;
        if(currentState != null)
            currentState.Leave();

        currentState = states[stateID];
        currentState.Enter();
    }

    public void AddState(StateId stateId, State state)
    {
        states.Add(stateId, state);    
    }
}
