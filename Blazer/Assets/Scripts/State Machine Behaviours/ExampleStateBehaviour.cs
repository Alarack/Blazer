using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleStateBehaviour : StateMachineBehaviour {

    public string stateName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //Debug.Log("Entering " + stateName);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        //Debug.Log(stateName + Mathf.Repeat( stateInfo.normalizedTime, 1f) + " is the time");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);

        //Debug.Log("Exiting " + stateName);
    }

}
