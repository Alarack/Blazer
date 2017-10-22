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

    protected Projectile parentProjectile;
    protected float rotateSpeed;

    protected override void Awake() {
        base.Awake();

        parentProjectile = GetComponent<Projectile>();
    }


    public override void Initialize() {
        base.Initialize();

        maxSpeed = parentProjectile.stats.GetStatCurrentValue(Constants.BaseStatType.MoveSpeed);
        rotateSpeed = parentProjectile.stats.GetStatCurrentValue(Constants.BaseStatType.RotateSpeed);

    }


    protected override void Move() {

        switch (direction) {

            case Direction.Up:
                myBody.velocity = transform.up * maxSpeed * Time.deltaTime;
                break;



        }
    }
}
