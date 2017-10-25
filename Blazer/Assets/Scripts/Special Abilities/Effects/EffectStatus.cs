using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EffectStatus : Effect {


    public Constants.StatusEffectType statusType;


    public float duration;
    public float interval;


 
    public override void Activate() {
        base.Activate();

        Debug.Log("Activaing status effect");

        BeginDelivery();
    }


    public override void Apply(GameObject target) {
        base.Apply(target);

        target.AddComponent<Status>();
        Status newStatus = target.GetComponent<Status>();

        newStatus.Initialize(target, duration, interval, statusType);

    }






}
