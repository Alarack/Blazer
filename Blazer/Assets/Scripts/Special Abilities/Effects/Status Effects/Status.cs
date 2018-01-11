using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status {

    public Constants.StatusEffectType statusType;
    public int StackCount { get; protected set; } 
    public int maxStack;

    protected Timer durationTimer;
    protected Timer intervalTimer;
    protected GameObject target;
    protected Entity source;
    protected Entity targetEntity;
    protected SpecialAbility sourceAbility;



    public virtual void Initialize(GameObject target, float duration, float interval, Constants.StatusEffectType statusType, SpecialAbility sourceAbility, int maxStack = 1) {
        this.target = target;
        this.statusType = statusType;
        this.sourceAbility = sourceAbility;
        this.maxStack = maxStack;

        targetEntity = target.GetComponent<Entity>();

        durationTimer = new Timer("Duration", duration, false, CleanUp);
        intervalTimer = new Timer("Interval", interval, true, Tick);
    }

    public virtual bool IsFromSameSource(SpecialAbility ability) {
        return sourceAbility == ability;
    }

    public virtual void Stack() {
        Debug.Log("Stacking");
        StackCount++;
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

    public virtual void ManagedUpdate() {
        durationTimer.UpdateClock();
        intervalTimer.UpdateClock();
    }

    protected virtual void Tick() {
        //Debug.Log("Tickin");

    }

    protected virtual void CleanUp() {
        //Debug.Log("Cleaning " + sourceAbility.abilityName);
        //Destroy(this);
        StatusManager.RemoveStatus(targetEntity, this);

    }




}
