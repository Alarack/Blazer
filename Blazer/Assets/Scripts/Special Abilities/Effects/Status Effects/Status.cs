using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public Constants.StatusEffectType statusType;
    public int stackCount = 1;

    protected Timer durationTimer;
    protected Timer intervalTimer;
    protected GameObject target;
    protected Entity source;
    protected Entity targetEntity;
    protected SpecialAbility sourceAbility;



    public virtual void Initialize(GameObject target, float duration, float interval, Constants.StatusEffectType statusType, SpecialAbility sourceAbility) {
        this.target = target;
        this.statusType = statusType;
        this.sourceAbility = sourceAbility;

        targetEntity = target.GetComponent<Entity>();

        durationTimer = new Timer("Duration", duration, false, CleanUp);
        intervalTimer = new Timer("Interval", interval, true, Tick);
    }

    public virtual bool IsFromSameSource(SpecialAbility ability) {
        return sourceAbility == ability;
    }

    public virtual void Stack() {
        Debug.Log("Stacking");
        stackCount++;
    }

    public virtual void RefreshDuration() {
        durationTimer.ResetTimer();
    }

    public virtual void ModifyIntervalTime(float mod) {
        intervalTimer.ModifyDuration(mod);
    }

    public virtual void ModifyDuration(float mod) {
        durationTimer.ModifyDuration(mod);
    }

    protected virtual void Update() {
        durationTimer.UpdateClock();
        intervalTimer.UpdateClock();
    }

    protected virtual void Tick() {
        //Debug.Log("Tickin");

    }

    protected virtual void CleanUp() {
        Debug.Log("Cleaning " + sourceAbility.abilityName);
        Destroy(this);
    }




}
