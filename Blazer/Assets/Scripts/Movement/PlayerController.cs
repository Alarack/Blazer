using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityMovement {

    //[Header("Temp Stats")]
    ////public int maxJumps = 1;
    //private int currentJumps;
    private bool isJumping = false;




    private void Update() {
        currentSpeed = Input.GetAxisRaw("Horizontal") * maxSpeed * Time.deltaTime;
        CheckFacing();
        TryJump();
    }


    protected override void Move() {
        myBody.velocity = new Vector2(currentSpeed, myBody.velocity.y);
        Jump();
    }

  
    private void TryJump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
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
