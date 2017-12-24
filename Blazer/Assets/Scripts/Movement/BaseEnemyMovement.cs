using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyMovement : EntityMovement {


    protected AIBrain brain;


    public override void Initialize() {
        base.Initialize();

        brain = GetComponent<AIBrain>();
        brain.State = AIBrain.EnemyState.Walking;
    }



    protected virtual void Update() {
        switch (brain.State) {
            case AIBrain.EnemyState.Walking:
                currentSpeed = maxSpeed;
                //Walk();
                break;

            case AIBrain.EnemyState.None:
                currentSpeed = 0f;

                break;

            default:
                currentSpeed = 0f;
                break;
        }



        SetWalkAnimation();

        CheckFacing();
    }


    protected virtual void SetWalkAnimation() {
        if (currentSpeed != 0f && !owner.MyAnimator.GetBool("Walk")) {
            owner.MyAnimator.SetBool("Walk", true);
            Debug.Log("Start Walk");
        }
        else if (currentSpeed == 0f && owner.MyAnimator.GetBool("Walk")) {
            owner.MyAnimator.SetBool("Walk", false);
            Debug.Log("Stop Walk");
        }
    }



    protected override void Move() {
        myBody.velocity = new Vector2(currentSpeed, myBody.velocity.y);


    }
}
