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
                Status newStatus = target.AddComponent<Status>();

                newStatus.Initialize(target, duration, interval, statusType, parentAbility);
                break;

            case Constants.StatusEffectType.AffectMovement:
                AffectMovement newAffectMovement = target.AddComponent<AffectMovement>();

                knockbackVector = TargetingUtilities.DegreeToVector2(knocbackAngle);

                if (Source.Facing == Constants.EntityFacing.Left) {
                    knockbackVector = new Vector2(-knockbackVector.x, knockbackVector.y);
                }

                newAffectMovement.Initialize(target, duration, interval, statusType, parentAbility);
                newAffectMovement.InitializeAffectMovement(affectMoveType, affectMoveValue, knockbackVector);
                break;

            case Constants.StatusEffectType.DamageOverTime:
                DamageOverTime[] existingDots = target.GetComponents<DamageOverTime>();

                for(int i = 0; i < existingDots.Length; i++) {
                    DamageOverTime existingDot = existingDots[i];

                    if (existingDot.IsFromSameSource(parentAbility)) {
                        switch (stackMethod) {
                            case Constants.StatusStackingMethod.None:
                                return;

                            case Constants.StatusStackingMethod.LimitedStacks:
                                if (existingDot.stackCount < maxStack) {
                                    existingDot.Stack();
                                }
                                else {
                                    existingDot.RefreshDuration();
                                }
                                return;

                            case Constants.StatusStackingMethod.StacksWithOtherAbilities:
                                return;
                        }
                    }
                }

                float damage;
                if (scaleFromBaseDamage)
                    damage = damagePerInterval + (parentAbility.source.stats.GetStatModifiedValue(Constants.BaseStatType.BaseDamage) * percentOfBaseDamage);
                else
                    damage = damagePerInterval;

                DamageOverTime newDot = target.AddComponent<DamageOverTime>();
                newDot.Initialize(target, duration, interval, statusType, parentAbility);
                newDot.InitializeDamageOverTime(damage, parentAbility.source);

                break;
        }



    }






}
