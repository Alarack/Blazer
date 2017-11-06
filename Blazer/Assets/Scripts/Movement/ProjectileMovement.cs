using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class ProjectileMovement : BaseMovement {

    public enum Direction {
        Up,
        Down,
        Left,
        Right,
        Still,
        Directed,
        None
    }

    public Direction direction;
    public bool lobbed;
    public float angle;

    protected Projectile parentProjectile;
    protected float rotateSpeed;

    protected override void Awake() {
        base.Awake();

        parentProjectile = GetComponent<Projectile>();
    }


    public override void Initialize() {
        base.Initialize();

        UpdateBaseStats();


        if (lobbed) {
            direction = Direction.None;

           Vector2 lobDirection = TargetingUtilities.DegreeToVector2(angle);

            if (parentProjectile.ParentFacing == Constants.EntityFacing.Left) {
                lobDirection = new Vector2(-lobDirection.x, lobDirection.y);
            }

            myBody.AddForce(lobDirection * maxSpeed);

        }

    }

    public override void UpdateBaseStats() {
        base.UpdateBaseStats();

        maxSpeed = parentProjectile.stats.GetStatModifiedValue(Constants.BaseStatType.MoveSpeed);
        rotateSpeed = parentProjectile.stats.GetStatModifiedValue(Constants.BaseStatType.RotateSpeed);
    }


    protected override void Move() {

        switch (direction) {

            case Direction.Up:
                myBody.velocity = transform.up * maxSpeed * Time.deltaTime;
                break;

        }
    }
}
