using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EffectStatus : Effect {

    //Base Stats
    public Constants.StatusEffectType statusType;
    public Constants.StatusStackingMethod stackMethod;
    public int maxStack = 1;
    public float duration;
    public float interval;

    //Affect Movement
    public AffectMovement.AffectMovementType affectMoveType;
    public float affectMoveValue;
    public float knocbackAngle;
    protected Vector2 knockbackVector;

    //Damage Over Time
    public bool scaleFromBaseDamage;
    public float damagePerInterval;
    public float percentOfBaseDamage;

    //Static Stat Adjustment
    public Constants.BaseStatType statType;
    public float statAdjustmentValue;
    public StatCollection.StatModificationType modType;

    public EffectStatus() {

    }

    public EffectStatus(EffectStatus effectStatus) {
        effectName = effectStatus.effectName;
        riderTarget = effectStatus.riderTarget;
        effectType = effectStatus.effectType;
        deliveryMethod = effectStatus.deliveryMethod;
        animationTrigger = effectStatus.animationTrigger;

        applyToSpecificTarget = effectStatus.applyToSpecificTarget;
        targetIndex = effectStatus.targetIndex;
        riders = effectStatus.CloneRiders();

        scaleFromBaseDamage = effectStatus.scaleFromBaseDamage;
        percentOfBaseDamage = effectStatus.percentOfBaseDamage;
        damagePerInterval = effectStatus.damagePerInterval;

        statusType = effectStatus.statusType;
        stackMethod = effectStatus.stackMethod;
        maxStack = effectStatus.maxStack;
        duration = effectStatus.duration;
        interval = effectStatus.interval;

        affectMoveType = effectStatus.affectMoveType;
        affectMoveValue = effectStatus.affectMoveValue;
        knocbackAngle = effectStatus.knocbackAngle;

        statType = effectStatus.statType;
        statAdjustmentValue = effectStatus.statAdjustmentValue;
        modType = effectStatus.modType;



        switch (deliveryMethod) {
            case Constants.EffectDeliveryMethod.Melee:
                meleeDelivery.prefabName = effectStatus.meleeDelivery.prefabName;
                meleeDelivery.layerMask = effectStatus.meleeDelivery.layerMask;
                break;

            case Constants.EffectDeliveryMethod.Projectile:
                projectileDelivery.prefabName = effectStatus.projectileDelivery.prefabName;
                projectileDelivery.layerMask = effectStatus.projectileDelivery.layerMask;
                break;
        }

    }




    public override void Activate() {
        base.Activate();
        BeginDelivery();
    }

    public override void Apply(GameObject target) {
        base.Apply(target);

        if (!CheckForSpecificTarget(target))
            return;

        switch (statusType) {
            case Constants.StatusEffectType.None:
                //Status newStatus = target.AddComponent<Status>();
                Status newStatus = new Status();


                newStatus.Initialize(target, duration, interval, statusType, parentAbility);

                StatusManager.AddStatus(target.GetComponent<Entity>(), newStatus, this, parentAbility); //HERE IS THE TEST LINE
                break;

            case Constants.StatusEffectType.AffectMovement:
                //AffectMovement newAffectMovement = target.AddComponent<AffectMovement>();
                AffectMovement newAffectMovement = new AffectMovement();

                knockbackVector = TargetingUtilities.DegreeToVector2(knocbackAngle);

                if (Source.Facing == Constants.EntityFacing.Left) {
                    knockbackVector = new Vector2(-knockbackVector.x, knockbackVector.y);
                }

                newAffectMovement.Initialize(target, duration, interval, statusType, parentAbility);
                newAffectMovement.InitializeAffectMovement(affectMoveType, affectMoveValue, knockbackVector);

                StatusManager.AddStatus(target.GetComponent<Entity>(), newAffectMovement, this, parentAbility); //HERE IS THE TEST LINE
                break;

            case Constants.StatusEffectType.Stun:
                Stun newStun = new Stun();

                newStun.Initialize(target, duration, interval, statusType, parentAbility);
                newStun.InitializeStun();

                StatusManager.AddStatus(target.GetComponent<Entity>(), newStun, this, parentAbility);
                break;

            case Constants.StatusEffectType.DamageOverTime:
                //DamageOverTime[] existingDots = target.GetComponents<DamageOverTime>();

                //for(int i = 0; i < existingDots.Length; i++) {
                //    DamageOverTime existingDot = existingDots[i];

                //    if (existingDot.IsFromSameSource(parentAbility)) {
                //        switch (stackMethod) {
                //            case Constants.StatusStackingMethod.None:
                //                return;

                //            case Constants.StatusStackingMethod.LimitedStacks:
                //                if (existingDot.stackCount < maxStack) {
                //                    existingDot.Stack();
                //                }
                //                else {
                //                    existingDot.RefreshDuration();
                //                }
                //                return;

                //            case Constants.StatusStackingMethod.StacksWithOtherAbilities:
                //                return;
                //        }
                //    }
                //}

                float damage;
                if (scaleFromBaseDamage)
                    damage = damagePerInterval + (parentAbility.source.stats.GetStatModifiedValue(Constants.BaseStatType.BaseDamage) * percentOfBaseDamage);
                else
                    damage = damagePerInterval;

                //DamageOverTime newDot = target.AddComponent<DamageOverTime>();
                DamageOverTime newDot = new DamageOverTime();
                newDot.Initialize(target, duration, interval, statusType, parentAbility, maxStack);
                newDot.InitializeDamageOverTime(damage, parentAbility.source);

                StatusManager.AddStatus(target.GetComponent<Entity>(), newDot, this, parentAbility); //HERE IS THE TEST LINE

                break;

            case Constants.StatusEffectType.StaticStatAdjustment:
                StatCollection.StatModifer mod = new StatCollection.StatModifer(statAdjustmentValue, modType);

                CombatManager.ApplyTrackedStatMod(Source, target.GetComponent<Entity>(), statType, mod);

                break;
        }



    }






}
