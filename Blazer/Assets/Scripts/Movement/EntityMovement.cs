using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public abstract class EntityMovement : MonoBehaviour {

    [Header("Basic Movement Stats")]
    public float maxSpeed;
    public float jumpForce;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.01f;
    public LayerMask whatIsGround;

    protected Rigidbody2D myBody;
    protected Entity owner;
    //protected SpriteRenderer mySprite;
    protected float currentSpeed;
    protected bool isGrounded;

    protected virtual void Awake () {
        myBody = GetComponent<Rigidbody2D>();
        owner = GetComponent<Entity>();
        //mySprite = GetComponentInChildren<SpriteRenderer>();
    }


    

    protected virtual void FixedUpdate() {
        if (maxSpeed != 0f)
            Move();

        CheckGround();
    }

    protected virtual void CheckGround() {
        if (groundCheck == null)
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    }

    protected void CheckFacing() {
        if (owner.SpriteRenderer == null)
            return;

        if (currentSpeed > 0 && owner.SpriteRenderer.flipX) {
            owner.SpriteRenderer.flipX = false;
            owner.Facing = Constants.EntityFacing.Right;
        }
        else if (currentSpeed < 0 && !owner.SpriteRenderer.flipX) {
            owner.SpriteRenderer.flipX = true;
            owner.Facing = Constants.EntityFacing.Left;
        }
    }

    protected abstract void Move();
}
