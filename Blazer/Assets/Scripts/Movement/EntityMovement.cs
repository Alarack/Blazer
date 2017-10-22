using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class EntityMovement : BaseMovement {


    protected float jumpForce;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.01f;
    public LayerMask whatIsGround;

    protected bool isGrounded;
    protected Entity owner;





    protected override void Awake () {
        base.Awake();
        owner = GetComponent<Entity>();
    }

    public override void Initialize() {
        base.Initialize();

        maxSpeed = owner.stats.GetStatCurrentValue(Constants.BaseStatType.MoveSpeed);
        jumpForce = owner.stats.GetStatCurrentValue(Constants.BaseStatType.JumpForce);

    }
    

    protected override void FixedUpdate() {
        base.FixedUpdate();
        
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


}
