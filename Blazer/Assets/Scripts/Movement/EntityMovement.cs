﻿using System.Collections;
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

    [Header("Sprite Pivot Hack")]
    public bool useSpritePivotHack;

    protected Vector2 spriteOffset;

    protected bool isGrounded;
    protected Entity owner;





    protected override void Awake () {
        base.Awake();
        owner = GetComponent<Entity>();
    }

    protected override void RegisterListeners() {
        base.RegisterListeners();

        Grid.EventManager.RegisterListener(Constants.GameEvent.StatChanged, OnStatChanged);
    }

    protected virtual void OnStatChanged(EventData data) {
        Constants.BaseStatType stat = (Constants.BaseStatType)data.GetInt("Stat");
        Entity target = data.GetMonoBehaviour("Target") as Entity;

        if (target != owner)
            return;

        switch (stat) {
            case Constants.BaseStatType.MoveSpeed:
                maxSpeed = owner.stats.GetStatCurrentValue(Constants.BaseStatType.MoveSpeed);
                break;

            case Constants.BaseStatType.JumpForce:
                jumpForce = owner.stats.GetStatCurrentValue(Constants.BaseStatType.JumpForce);
                break;
        }


    }

    public override void Initialize() {
        base.Initialize();

        maxSpeed = owner.stats.GetStatCurrentValue(Constants.BaseStatType.MoveSpeed);
        jumpForce = owner.stats.GetStatCurrentValue(Constants.BaseStatType.JumpForce);

        float spriteOffsetX = owner.SpriteRenderer.bounds.size.x;

        spriteOffset = new Vector2(spriteOffsetX, 0f);
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
            //owner.SpriteRenderer.transform.localScale = Vector3.one;
            owner.Facing = Constants.EntityFacing.Right;
            //Debug.Log("Flipping right " + owner.SpriteRenderer.flipX);

            if (useSpritePivotHack) {
                owner.SpriteRenderer.gameObject.transform.localPosition -= (Vector3)spriteOffset;
            }

        }
        else if (currentSpeed < 0 && !owner.SpriteRenderer.flipX) {
            owner.SpriteRenderer.flipX = true;
            //owner.SpriteRenderer.transform.localScale = new Vector3(-1f, 1f, 1f);
            owner.Facing = Constants.EntityFacing.Left;

            if (useSpritePivotHack) {
                owner.SpriteRenderer.gameObject.transform.localPosition += (Vector3)spriteOffset;
            }

            //Debug.Log("Flipping Left " + owner.SpriteRenderer.flipX);
        }
    }


}
