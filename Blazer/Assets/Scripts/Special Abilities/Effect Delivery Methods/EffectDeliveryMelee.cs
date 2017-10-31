﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectDeliveryMelee : EffectDeliveryMethod {

    public string prefabName;




    public override void Deliver() {
        base.Deliver();

        //CreateMeleeAttack();

        Grid.EventManager.RegisterListener(Constants.GameEvent.AnimationEvent, OnAnimationEvent);
    }



    private void ConfigureMeleeAttack() {
        switch (targetingMethod) {
            case TargetingMethod.StraightLeftRight:
                if (parentAbility.source.Facing == Constants.EntityFacing.Left) {
                    shootDirection = Vector2.left;
                    effectOrigin = parentAbility.source.leftShotOrigin.position;
                    //shotPos = parentAbility.source.leftShotOrigin;
                }
                else {
                    shootDirection = Vector2.right;
                    effectOrigin = parentAbility.source.rightShotOrigin.position;
                    //shotPos = parentAbility.source.rightShotOrigin;
                }
                break;
        }
    }




    private void OnAnimationEvent(EventData data) {
        //Debug.Log("Recieving Attack");

        Entity owner = data.GetMonoBehaviour("Entity") as Entity;
        string attackName = data.GetString("AttackName");

        if (owner != parentAbility.source)
            return;

        if (attackName != parentEffect.animationTrigger)
            return;

        CreateMeleeAttack();

    }



    private void CreateMeleeAttack() {
        Grid.EventManager.RemoveListener(Constants.GameEvent.AnimationEvent, OnAnimationEvent);


        ConfigureMeleeAttack();

        GameObject loadedPrefab = Resources.Load("Melee/" + prefabName) as GameObject;

        if (loadedPrefab == null) {
            Debug.LogError("Prefab was null");
            return;
        }

        GameObject hit = VisualEffectManager.CreateVisualEffect(loadedPrefab, effectOrigin, Quaternion.identity);
        MeleeHit hitScript = hit.GetComponent<MeleeHit>();

        hit.transform.SetParent(parentAbility.source.transform, true);

        hitScript.Initialize(parentEffect, layerMask, 0f, parentEffect.effectDamage);

    }



}
