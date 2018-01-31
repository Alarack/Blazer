using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Status {


    protected AIBrain targetBrain;

    public override void Initialize(GameObject target, float duration, float interval, Constants.StatusEffectType statusType, SpecialAbility sourceAbility, int maxStack = 1, Effect onCompleteEffect = null)
    {
        base.Initialize(target, duration, interval, statusType, sourceAbility, maxStack);
        targetBrain = target.GetComponent<AIBrain>();
    }

    public void InitializeStun()
    {
        targetBrain.gameObject.GetComponent<AIStateMachine>().ChangeState(AIStateMachine.AIState.Stunned, true);
    }

    protected override void CleanUp()
    {
        if (targetBrain == null)
        {
            //Debug.Log("Nove moves");
            StatusManager.RemoveStatus(targetEntity, this);
            //Destroy(this);
            return;
        }
        targetBrain.myStateMachine.myState = AIStateMachine.AIState.Alerted;

        base.CleanUp();
    }
}
