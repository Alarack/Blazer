using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyMovement : EntityMovement {

    public float paceTime = 2f;

    protected Timer paceTimer;

    protected AIBrain brain;

    protected float facingMod = 1f;

    public override void Initialize() {
        base.Initialize();

        brain = GetComponent<AIBrain>();
        brain.State = AIBrain.EnemyState.Walking;

        paceTimer = new Timer("Pace Timer", paceTime, true, Flip);
    }



    protected virtual void Update() {

        if (paceTimer != null)
            paceTimer.UpdateClock();

        switch (brain.State) {
            case AIBrain.EnemyState.Walking:
                currentSpeed = maxSpeed * facingMod;
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

    public void Flip() {
        facingMod *= -1;
    }
}
