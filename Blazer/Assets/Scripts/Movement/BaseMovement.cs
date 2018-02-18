using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseMovement : MonoBehaviour {

    public bool CanMove { get; set; }
    public bool CanPivot { get; set; }

    protected float maxSpeed;
    protected float currentSpeed;
    protected Rigidbody2D myBody;

    protected virtual void Awake() {
        myBody = GetComponent<Rigidbody2D>();
    }

    public virtual void Initialize() {
        RegisterListeners();

        CanMove = true;
        CanPivot = true;
    }

    protected virtual void RegisterListeners() {

    }

    public virtual void RemoveMyListeners() {
        EventGrid.EventManager.RemoveMyListeners(this);
    }

    public virtual void UpdateBaseStats() {

    }


    protected virtual void FixedUpdate() {
        if (maxSpeed == 0f)
            return;

        if (!CanMove)
            return;

            Move();
    }


    protected abstract void Move();

}
