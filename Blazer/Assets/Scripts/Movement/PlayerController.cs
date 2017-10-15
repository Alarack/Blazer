using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityMovement {

    [Header("Temp Stats")]
    //public int maxJumps = 1;
    public LayerMask laserHitMask;
    public GameObject defaultHitparticle;
    //public Transform weaponPoint;

    //private int currentJumps;
    private bool isJumping = false;

    private void Update() {
        currentSpeed = Input.GetAxis("Horizontal") * maxSpeed * Time.deltaTime;
        CheckFacing();
        TryJump();

        //if (Input.GetMouseButtonDown(0)) {
        //    TryShootRay();
        //}
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


    private void TryShootRay() {


        Vector2 shootDirection;
        Vector2 shotOrigin;

        if (owner.Facing == Constants.EntityFacing.Left) {
            shootDirection = Vector2.left;
            shotOrigin = owner.leftShotOrigin.position;
        }
        else {
            shootDirection = Vector2.right;
            shotOrigin = owner.rightShotOrigin.position;
        }


        RaycastHit2D hit = Physics2D.Raycast(shotOrigin, shootDirection, Mathf.Infinity, laserHitMask);


        if(hit.collider != null) {
            Debug.Log(hit.collider.gameObject.name + " was hit");

            Vector2 rayDir = Vector2.Reflect((hit.point - shotOrigin).normalized, hit.normal);

            float angle = Mathf.Atan2(-rayDir.x, rayDir.y) * Mathf.Rad2Deg;

            Quaternion rot = Quaternion.AngleAxis(angle, Vector2.right);

            GameObject hitEffect = Instantiate(defaultHitparticle, hit.point, rot) as GameObject;


            hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-rayDir * 150f);

            Destroy(hitEffect, 1f);

        }

    }
}
