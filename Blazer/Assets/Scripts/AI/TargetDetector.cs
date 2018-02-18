using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector {

    protected GameObject target = null;
    private AIBrain myBrain;

    protected override void Awake()
    {
        base.Awake();
        myBrain = GetComponentInParent<AIBrain>();
        //Debug.Log(myBrain);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        detectedLayers = myBrain.targetLayers;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

    protected override void EnterDetectorFunction(Collider2D collision)
    {
        base.EnterDetectorFunction(collision);
        Debug.Log(target);
        if (detectedLayers.Contains(collision.gameObject.layer) && target == null)
        {
            target = collision.gameObject;
            Debug.Log(target);
            if (myBrain.myStateMachine.myState == AIStateMachine.AIState.Idle && myBrain.myStateMachine.canChangeState)
            {
                myBrain.myStateMachine.ChangeState(AIStateMachine.AIState.Alerted, false);
            }
        }
    }
    protected override void StayDetectorFunction(Collider2D collision)
    {
        base.StayDetectorFunction(collision);
        if (target != null)
        {
            /*--Horizontal Direction Checkers--*/
            if (target.transform.position.x - gameObject.transform.position.x < -0.5f)
            {
                //Debug.Log("Left");
                //Debug.Log(myBrain.targetDirection);
            }
            if (target.transform.position.x - gameObject.transform.position.x > 0.5f)
            {
                //Debug.Log("Right");
                myBrain.targetDirection.horizontalDirection = AIBrain.Direction.Right;
            }
            if (target.transform.position.x - gameObject.transform.position.x >= -0.5f && target.transform.position.x - gameObject.transform.position.x <= 0.5f)
            {
                myBrain.targetDirection.horizontalDirection = AIBrain.Direction.None;
            }
            /*--Vertical Direction Checkers--*/
            if (target.transform.position.y - gameObject.transform.position.y > 0.5f)
            {
                //Debug.Log("Up");
                myBrain.targetDirection.verticalDirection = AIBrain.Direction.Up;
            }
            if (target.transform.position.y - gameObject.transform.position.y < -0.5f)
            {
                //Debug.Log("Up");
                myBrain.targetDirection.verticalDirection = AIBrain.Direction.Down;
            }
            if (target.transform.position.y - gameObject.transform.position.y >= -0.5f && target.transform.position.y - gameObject.transform.position.y <= 0.5f)
            {
                myBrain.targetDirection.verticalDirection = AIBrain.Direction.None;
            }
        }

    }
    protected override void ExitDetectorFunction(Collider2D collision)
    {
        base.ExitDetectorFunction(collision);
        if (collision.gameObject == target)
        {
            //Debug.Log("None");
            target = null;
            myBrain.myStateMachine.ChangeState(AIStateMachine.AIState.Idle, false);
        }
    }
}
