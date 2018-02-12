using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityMovement {

    public Timer fallthroughTimer;
    public float disableDuration;

    private bool isJumping = false;
    private bool isFallingThrough = false;

    public override void Initialize() {
        base.Initialize();
        fallthroughTimer = new Timer("fallthroughTimer", disableDuration, true, DisableFallthrough);

        if (Platformed && Input.GetAxisRaw("Vertical") < 0) {
            isFallingThrough = true;
        }
    }


    private void Update() {
        /*--All the controls during climbing. This was moved in front of the walking controls and added an if statement to make sure that players don't
         * A) walk off ladders while climbing
         * b) trigger a bug that causes anything that grabs a ladder to float off into space in the direction of their initial movement--*/
        if (IsClimbing)
        {
            if (Input.GetAxisRaw("Vertical") >= 1)
            {
                myBody.velocity = new Vector2(currentSpeed, ascendSpeed);
                owner.MyAnimator.SetBool("Climbing", true);
            }
            if (Input.GetAxisRaw("Vertical") <= -1)
            {
                myBody.velocity = new Vector2(currentSpeed, -descendSpeed);
                owner.MyAnimator.SetBool("Climbing", true);
            }
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                myBody.velocity = new Vector2(currentSpeed, 0);
                owner.MyAnimator.SetBool("Climbing", false);
            }
            if (Input.GetButtonDown("Jump"))
            {
                ClimbEnd();
                isJumping = true;
            }
        }
        else if (!IsClimbing)
        {
            currentSpeed = Input.GetAxisRaw("Horizontal") * maxSpeed;

            if (currentSpeed != 0f && !owner.MyAnimator.GetBool("Walking"))
            {
                owner.MyAnimator.SetBool("Walking", true);
            }
            else if (currentSpeed == 0f && owner.MyAnimator.GetBool("Walking"))
            {
                owner.MyAnimator.SetBool("Walking", false);
            }

            if (Platformed && Input.GetAxisRaw("Vertical") < 0)
            {
                isFallingThrough = true;
            }
        }


        CheckFacing();
        TryJump();
        Fallthrough(isFallingThrough);
        fallthroughTimer.UpdateClock();
        
        if(!Grounded && !Platformed)
        {
            owner.MyAnimator.SetBool("InAir", true);
        }
        if(Grounded || Platformed)
        {
            owner.MyAnimator.SetBool("InAir", false);
        }

        /*--Essentially just tells the player when to start climbing--*/
        if (canClimb)
        {
            if (!IsClimbing)
            {
                if (Input.GetAxisRaw("Vertical") >= 1 || Input.GetAxisRaw("Vertical") <= -1)
                {
                    ClimbBeginning(currentLadder);
                }
            }
        }
        //Debug.Log(Grounded + " is the status of Grounded");
        //Debug.Log(Platformed + " is the status of platformed");
    }


    protected override void Move() {
        myBody.velocity = new Vector2(currentSpeed, myBody.velocity.y);
        Jump();
    }


    private void TryJump() {
        if (Input.GetButtonDown("Jump") && (Grounded || Platformed)) {
            owner.MyAnimator.SetTrigger("Jumping");
            isJumping = true;
        }
    }

    private void Jump() {
        if (isJumping) {
            myBody.AddForce(Vector2.up * jumpForce);
            isJumping = false;
        }

    }

    private void DisableFallthrough() {
        isFallingThrough = false;
    }

}
