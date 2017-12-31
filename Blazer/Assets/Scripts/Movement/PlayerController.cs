using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityMovement {

    public Timer fallthroughTimer;
    public float disableDuration;

    private bool isJumping = false;
    private bool isFallingThrough = false;

    public override void Initialize()
    {
        base.Initialize();
        fallthroughTimer = new Timer("fallthroughTimer", disableDuration, true, DisableFallthrough);
    }


    private void Update() {
        currentSpeed = Input.GetAxisRaw("Horizontal") * maxSpeed;

        if (currentSpeed != 0f && !owner.MyAnimator.GetBool("Walking")) {
            owner.MyAnimator.SetBool("Walking", true);
        }
        else if (currentSpeed == 0f && owner.MyAnimator.GetBool("Walking")) {
            owner.MyAnimator.SetBool("Walking", false);
        }

        if (Platformed && Input.GetAxisRaw("Vertical") < 0)
        {
            isFallingThrough = true;
        }

        CheckFacing();
        TryJump();
        Fallthrough(isFallingThrough);
        fallthroughTimer.UpdateClock();


        //Debug.Log(Grounded + " is the status of Grounded");
        //Debug.Log(Platformed + " is the status of platformed");
    }


    protected override void Move() {
        myBody.velocity = new Vector2(currentSpeed, myBody.velocity.y);
        Jump();
    }


    private void TryJump() {
        if (Input.GetButtonDown("Jump") && (Grounded || Platformed)) {
            isJumping = true;
        }
    }

    private void Jump() {
        if (isJumping) {
            myBody.AddForce(Vector2.up * jumpForce);
            isJumping = false;
        }

    }

    private void DisableFallthrough()
    {
        isFallingThrough = false;
    }
}

    public override void Initialize() {
        base.Initialize();
        fallthroughTimer = new Timer("fallthroughTimer", disableDuration, true, DisableFallthrough);
        if (Platformed && Input.GetAxisRaw("Vertical") < 0) {
            isFallingThrough = true;
    private void DisableFallthrough() {
        isFallingThrough = false;