using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public Constants.StatusEffectType statusType;

    //public float duration;
    //public float interval;

    //protected float _durationTimer = 0f;
    //protected float _intervalTimer = 0f;

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
        //UpdateTimer(_durationTimer, duration);

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


    //protected virtual void UpdateTimer(float timer, float clock) {
    //    if(timer < clock) {
    //        timer += Time.deltaTime;
    //        Debug.Log(timer);
    //        if(timer >= clock) {
    //            timer = 0f;
    //            Debug.Log("Tick");

    //        }

    //    }


    //}


}
