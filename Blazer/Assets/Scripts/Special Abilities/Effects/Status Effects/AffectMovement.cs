﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectMovement : Status {

    public enum AffectMovementType {
        None,
        AlterSpeed,
        Halt,
        Knockback
    }


    protected BaseMovement targetMovement;
    protected AffectMovementType affectType;
    protected float amount;
    protected Vector2 knockbackVector;
    //protected List<StatCollection.StatModifer> mods = new List<StatCollection.StatModifer>();
    protected StatCollection.StatModifer mod;

    public override void Initialize(GameObject target, float duration, float interval, Constants.StatusEffectType statusType, SpecialAbility sourceAbility) {
        base.Initialize(target, duration, interval, statusType, sourceAbility);
    }

    public void InitializeAffectMovement(AffectMovementType type, float value, Vector2 knockback) {
        affectType = type;
        amount = value;
        targetMovement = target.GetComponent<BaseMovement>();

        switch (affectType) {
            case AffectMovementType.Halt:

                if (targetMovement == null)
                    return;

                if (targetMovement is EntityMovement) {
                    if (((EntityMovement)targetMovement).Grounded)
                        target.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }

                targetMovement.CanMove = false;
                break;

            case AffectMovementType.Knockback:
                target.GetComponent<Rigidbody2D>().AddForce(knockback * value);

                break;

            case AffectMovementType.AlterSpeed:
                mod = new StatCollection.StatModifer(amount, StatCollection.StatModificationType.Multiplicative);

                //Debug.Log("Applyin a mod of " + mod.value);

                CombatManager.ApplyTrackedStatMod(source, targetEntity, Constants.BaseStatType.MoveSpeed, mod);

                //CombatManager.ApplyUntrackedStatMod(source, target.GetComponent<Entity>(), Constants.BaseStatType.MoveSpeed, amount, StatCollection.StatModificationType.Multiplicative);
                break;
        }


    }


    protected override void CleanUp() {
        if (targetMovement == null) {
            //Debug.Log("Nove moves");
            Destroy(this);
            return;
        }

        switch (affectType) {
            case AffectMovementType.Halt:
                targetMovement.CanMove = true;
                break;

            case AffectMovementType.AlterSpeed:
                if(mod != null) {
                    CombatManager.RemoveTrackedStatMod(targetEntity, Constants.BaseStatType.MoveSpeed, mod);
                }
                else {
                    Debug.Log("Mod NUll");
                }
                break;
        }


        base.CleanUp();
    }

}
