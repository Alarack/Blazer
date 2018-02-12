using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStateMachine : MonoBehaviour {

    public NPCAbilityManager abilityManager;
    public AIState myState;
    public Timer lockDuration;

    public enum AIState
    {
        Idle = 0,
        Walking = 1,
        Attacking = 2,
        Stunned = 3,
        InAir = 4,
        Stopped = 5,
        Alerted = 6,
    }
    public bool canChangeState;

    protected void Awake()
    {
        abilityManager = GetComponent<AIBrain>().abilityManager;
    }




    public void LockState(float duration)
    {
        if (canChangeState)
        {
            Debug.Log("State Locked");
            canChangeState = false;
            lockDuration = new Timer("StateLockTimer", duration, false, UnlockState);
        }
    }

    public void UnlockState()
    {
        if (!canChangeState)
        {
            Debug.Log("State Unlocked");
            canChangeState = true;
        }
    }

    public void ChangeState(AIState changeTo, bool overrides = false)
    {
        if (canChangeState || overrides)
        {
            Debug.Log("Changed state to " + changeTo);
            myState = changeTo;
            if (overrides)
            {
                lockDuration = null;
                UnlockState();
            }
        }
        else if (!canChangeState && !overrides)
        {
            Debug.Log("Cannot Change State to " + changeTo + " due to state lock.");
        }
    }

}
