﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EffectStatus : Effect {


    public Constants.StatusEffectType statusType;


    public float duration;
    public float interval;

    //Affect Movement
    public AffectMovement.AffectMovementType affectMoveType;
    public float affectMoveValue;
    public float knocbackAngle;

    protected Vector2 knockbackVector;

 
    public override void Activate() {
        base.Activate();
        BeginDelivery();
    }


    public override void Apply(GameObject target) {
        base.Apply(target);

        switch (statusType) {
            case Constants.StatusEffectType.None:
                Status newStatus = target.AddComponent<Status>();

                newStatus.Initialize(target, duration, interval, statusType);
                break;

            case Constants.StatusEffectType.AffectMovement:
                AffectMovement newAffectMovement = target.AddComponent<AffectMovement>();

                knockbackVector = TargetingUtilities.DegreeToVector2(knocbackAngle);

                if(Source.Facing == Constants.EntityFacing.Left) {
                    knockbackVector = new Vector2(-knockbackVector.x, knockbackVector.y);
                }

                newAffectMovement.Initialize(target, duration, interval, statusType);
                newAffectMovement.InitializeAffectMovement(affectMoveType, affectMoveValue, knockbackVector);


                break;
        }



    }






}
