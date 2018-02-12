using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStateMachine : AIStateMachine {


    private void StateMachine()
    {
        switch (myState)
        {
            case AIState.Idle:                
                break;

            case AIState.Alerted:

                break;

            case AIState.Walking:

                break;

            case AIState.Attacking:
                
                break;

            case AIState.Stunned:

                break;

            case AIState.Stopped:

                break;

            default:
                break;
        }
    }

    protected void FixedUpdate()
    {
        Debug.Log(myState);
        StateMachine();
    }
}
