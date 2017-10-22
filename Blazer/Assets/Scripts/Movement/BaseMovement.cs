using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseMovement : MonoBehaviour {

    protected float maxSpeed;
    protected float currentSpeed;
    protected Rigidbody2D myBody;

    protected virtual void Awake() {
        myBody = GetComponent<Rigidbody2D>();
    }

    public virtual void Initialize() {
        RegisterListeners();

    }

    protected virtual void RegisterListeners() {

    }

    protected virtual void FixedUpdate() {
        if (maxSpeed != 0f)
            Move();

        //CheckGround();
    }


    protected abstract void Move();

}
