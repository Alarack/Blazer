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
        if (!isClimbing)
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
            if (canClimb && Input.GetAxisRaw("Vertical") > 0 && climbPoint.position.y <= (myLadder.ladderTop - 0.01f))
            {
                myLadder.GrabLadder(gameObject);
            }
            if (canClimb && Input.GetAxisRaw("Vertical") < 0 && climbPoint.position.y >= (myLadder.ladderBot + 0.01f))
            {
                myLadder.GrabLadder(gameObject);
            }
        }
        if (isClimbing)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                if (climbPoint.position.y > (myLadder.ladderTop - 0.01f))
                {
                    myLadder.LetGoLadder(gameObject);
                }
                else
                {
                    Debug.Log("Climbing Up");
                    myLadder.ClimbUp(myClimber);
                    myBody.position = new Vector2(myLadder.transform.position.x, myBody.position.y);
                    myBody.velocity = new Vector2(0f, ascendSpeed);
                }
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                if (climbPoint.position.y < (myLadder.ladderBot + 0.01f))
                {
                    myLadder.LetGoLadder(gameObject);
                }
                else
                {
                    Debug.Log("Climbing Down");
                    myLadder.ClimbDown(myClimber);
                    myBody.position = new Vector2(myLadder.transform.position.x, myBody.position.y);
                    myBody.velocity = new Vector2(0f, -descendSpeed);
                }
            }
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                //Debug.Log("Clinging On");
                myLadder.ClimbHold(myClimber);
                myBody.position = new Vector2(myLadder.transform.position.x, myBody.position.y);
                myBody.velocity = Vector2.zero;
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

        //Debug.Log(Grounded + " is the status of Grounded");
        //Debug.Log(Platformed + " is the status of platformed");
    }


    protected override void Move() {
        myBody.velocity = new Vector2(currentSpeed, myBody.velocity.y);
        Jump();
    }


    private void TryJump() {
        if (Input.GetButtonDown("Jump") && (Grounded || Platformed || isClimbing)) {
            if (isClimbing)
            {
                myLadder.LetGoLadder(gameObject);
            }
            if (Grounded || Platformed)
            {
                owner.MyAnimator.SetTrigger("Jumping");
            }
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
