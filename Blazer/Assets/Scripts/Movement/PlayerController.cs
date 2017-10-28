using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityMovement {


    private bool isJumping = false;




    private void Update() {
        currentSpeed = Input.GetAxisRaw("Horizontal") * maxSpeed * Time.deltaTime;

        if (currentSpeed != 0f && !owner.MyAnimator.GetBool("Walking")) {
            owner.MyAnimator.SetBool("Walking", true);
        }
        else if (currentSpeed == 0f && owner.MyAnimator.GetBool("Walking")) {
            owner.MyAnimator.SetBool("Walking", false);
        }


        CheckFacing();
        TryJump();
    }


    protected override void Move() {
        myBody.velocity = new Vector2(currentSpeed, myBody.velocity.y);
        Jump();
    }

  
    private void TryJump() {
        if (Input.GetButtonDown("Jump") && Grounded) {
            isJumping = true;
        }
    }

    private void Jump() {
        if (isJumping) {
            myBody.AddForce(Vector2.up * jumpForce);
            isJumping = false;
        }

    }
}
