using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public Constants.StatusEffectType statusType;


    protected Timer durationTimer;
    protected Timer intervalTimer;
    protected GameObject target;


    protected void Start() {
        //Initialize(gameObject);

    }

    public virtual void Initialize(GameObject target, float duration, float interval, Constants.StatusEffectType statusType) {
        this.target = target;
        this.statusType = statusType;

        durationTimer = new Timer("Duration", duration, false, CleanUp);
        intervalTimer = new Timer("Interval", interval, true, Tick);
    }


    protected virtual void Update() {
        durationTimer.UpdateClock();
        intervalTimer.UpdateClock();
    }


    protected virtual void Tick() {

        Debug.Log("Tickin");

    }


    protected virtual void CleanUp() {

        Debug.Log("Cleaning");

        Destroy(this);
    }


}
