using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpecialAbilityData))]
public class SpecialAbilityDataEditor : Editor {


    private SpecialAbilityData _abilityData;


    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        _abilityData = (SpecialAbilityData)target;


        //DrawPresets();


        _abilityData.abilityName = EditorGUILayout.TextField("Ability Name", _abilityData.abilityName);
        _abilityData.abilityIcon = EditorHelper.ObjectField<Sprite>("Icon", _abilityData.abilityIcon);
        EditorGUILayout.Separator();

        _abilityData.recoveryType = EditorHelper.EnumPopup("Recvery Type", _abilityData.recoveryType);

        DrawRecoveryMethod(_abilityData.recoveryManager, _abilityData.recoveryType);

        EditorGUILayout.Separator();

        _abilityData.useDuration = EditorGUILayout.FloatField("Use Duration", _abilityData.useDuration);
        _abilityData.overrideOtherAbilities = EditorGUILayout.Toggle("Override Other Abilities?", _abilityData.overrideOtherAbilities);

        EditorGUILayout.Separator();

        _abilityData.sequencedAbilities = EditorHelper.DrawList("Sequenced Abilities", _abilityData.sequencedAbilities, true, null, true, DrawSpecialAbilityData);

        if(_abilityData.sequencedAbilities.Count > 0) {
            _abilityData.sequenceWindow = EditorGUILayout.FloatField("Sequence Window", _abilityData.sequenceWindow);
        }

        EditorGUILayout.Separator();

        _abilityData.effectTypes = EditorHelper.DrawList("Effects", _abilityData.effectTypes, true, Constants.SpecialAbilityEffectType.None, true, DrawSpecialAbilityTypes);

        EditorGUILayout.Separator();

        for (int i = 0; i < _abilityData.effectTypes.Count; i++) {
            ShowEffectOfType(_abilityData.effectTypes[i], _abilityData.effectHolder);
        }


        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }



    private void ShowEffectOfType(Constants.SpecialAbilityEffectType effectType, SpecialAbilityData.EffectHolder effects) {

        switch (effectType) {
            case Constants.SpecialAbilityEffectType.AttackEffect:
                EditorGUILayout.Separator();

                effects.attacks = EditorHelper.DrawExtendedList("Attacks", effects.attacks, "Attack", DrawEffectList);

                break;

            case Constants.SpecialAbilityEffectType.StatusEffect:
                effects.statusEffects = EditorHelper.DrawExtendedList("Status Effects", effects.statusEffects, "Status", DrawEffectList);

                break;

        }

    }


    //private void DrawPresets() {
    //    if (GUILayout.Button("Add RayCast Attack")) {
    //        EffectRayCastAttack rayAttack = new EffectRayCastAttack();
    //        rayAttack.effectType = Constants.SpecialAbilityEffectType.RayCastAttack;
    //        _abilityData.testEffects.Add(rayAttack);
    //    }
    //}

    //private T DrawDeliveryMethodList<T>(T deliveryMethod) where T : EffectDeliveryMethod {


    //    return (T)DrawDeliveryMethod(deliveryMethod);
    //}




    private T DrawEffectList<T>(T effect) where T : Effect {

        return (T)DrawEffect(effect);
    }

    private Effect DrawEffect(Effect entry) {

        entry.deliveryMethod = EditorHelper.EnumPopup("Delivery method", entry.deliveryMethod);

        DrawDeliveryMethod(entry);


        entry.applyToSpecificTarget = EditorGUILayout.Toggle("Apply to Xth hit Target?", entry.applyToSpecificTarget);

        if (entry.applyToSpecificTarget) {
            entry.targetIndex = EditorGUILayout.IntField("Which Hit?", entry.targetIndex);
        }

        //entry.requireMultipleHits = EditorGUILayout.Toggle("Require Multiple Triggers?", entry.requireMultipleHits);
        //if (entry.requireMultipleHits) {
        //    entry.requiredHits = EditorGUILayout.IntField("Number of Triggers", entry.requiredHits);
        //}

        //Attacks
        if (entry is EffectAttack) {
            EffectAttack attackEffect = entry as EffectAttack;
            attackEffect.effectType = Constants.SpecialAbilityEffectType.AttackEffect;

            attackEffect.effectDamage = EditorGUILayout.IntField("Efect Base Damage", attackEffect.effectDamage);

            attackEffect.scaleFromBaseDamage = EditorGUILayout.Toggle("Scale From Entity base damage?", attackEffect.scaleFromBaseDamage);
            if (attackEffect.scaleFromBaseDamage) {
                attackEffect.percentOfBaseDamage = EditorHelper.PercentFloatField("Percent of base damage", attackEffect.percentOfBaseDamage);
            }
            EditorGUILayout.Separator();

            attackEffect.burstAttack = EditorGUILayout.Toggle("Burst?", attackEffect.burstAttack);

            if (attackEffect.burstAttack) {
                attackEffect.burstInterval = EditorGUILayout.FloatField("Delay between shots", attackEffect.burstInterval);
                attackEffect.burstNumber = EditorGUILayout.IntField("Number of shots", attackEffect.burstNumber);
                EditorGUILayout.Separator();
            }

            attackEffect.penetrate = EditorGUILayout.Toggle("Penetrating?", attackEffect.penetrate);
            if (attackEffect.penetrate) {
                attackEffect.numPenetrations = EditorGUILayout.IntField("Number of Penetrations (0 = INF)", attackEffect.numPenetrations);
            }

            EditorGUILayout.Separator();
            attackEffect.fireEffectName = EditorGUILayout.TextField("Fire Effect Name", attackEffect.fireEffectName);
            attackEffect.impactEffectName = EditorGUILayout.TextField("Impact Effect Name", attackEffect.impactEffectName);
        }

        //Status Effects
        if(entry is EffectStatus) {
            EffectStatus statusAttack = entry as EffectStatus;
            statusAttack.effectType = Constants.SpecialAbilityEffectType.StatusEffect;
            statusAttack.stackMethod = EditorHelper.EnumPopup("Stack Method", statusAttack.stackMethod);

            switch (statusAttack.stackMethod) {
                case Constants.StatusStackingMethod.LimitedStacks:
                    statusAttack.maxStack = EditorGUILayout.IntField("Max Stacks?", statusAttack.maxStack);
                    break;
            }

            statusAttack.statusType = EditorHelper.EnumPopup("Status Type", statusAttack.statusType);
            statusAttack.duration = EditorGUILayout.FloatField("Duration (0 = INF)", statusAttack.duration);
            statusAttack.interval = EditorGUILayout.FloatField("Interval Time", statusAttack.interval);


            switch (statusAttack.statusType) {
                case Constants.StatusEffectType.AffectMovement:
                    statusAttack.affectMoveType = EditorHelper.EnumPopup("Movement Affect Type", statusAttack.affectMoveType);

                    if(statusAttack.affectMoveType != AffectMovement.AffectMovementType.Halt)
                        statusAttack.affectMoveValue = EditorHelper.FloatField("Value", statusAttack.affectMoveValue);

                    if(statusAttack.affectMoveType == AffectMovement.AffectMovementType.Knockback) {
                        statusAttack.knocbackAngle = EditorHelper.FloatField("Angle", statusAttack.knocbackAngle);
                    }

                    break;

                case Constants.StatusEffectType.DamageOverTime:

                    statusAttack.damagePerInterval = EditorHelper.FloatField("Damage Per Interval", statusAttack.damagePerInterval);
                    statusAttack.scaleFromBaseDamage = EditorGUILayout.Toggle("Scale from Base Damage?", statusAttack.scaleFromBaseDamage);

                    if (statusAttack.scaleFromBaseDamage)
                        statusAttack.percentOfBaseDamage = EditorHelper.PercentFloatField("Percentage of Base Damage", statusAttack.percentOfBaseDamage);

                    break;
            }
        }


        return entry;
    }


    private void DrawRecoveryMethod(AbilityRecoveryManager entry, Constants.SpecialAbilityRecoveryType recoveryType) {
        switch (recoveryType) {
            case Constants.SpecialAbilityRecoveryType.Timed:
                entry.recoveryCooldown.cooldown = EditorGUILayout.FloatField("Cooldown", entry.recoveryCooldown.cooldown);
                entry.recoveryCooldown.recoveryType = Constants.SpecialAbilityRecoveryType.Timed;
                break;


        }



        //if (entry is RecoveryCooldown) {
        //    RecoveryCooldown cooldown = entry as RecoveryCooldown;
        //    cooldown.cooldown = EditorGUILayout.FloatField("Cooldown", cooldown.cooldown);
        //}
    }

    private void DrawDeliveryMethod(Effect effect) {

        EditorGUILayout.Separator();

        effect.effectName = EditorGUILayout.TextField("Effect Name", effect.effectName);
        effect.animationTrigger = EditorGUILayout.TextField("Animation Trigger", effect.animationTrigger);

        switch (effect.deliveryMethod) {
            case Constants.EffectDeliveryMethod.Raycast:
                effect.rayCastDelivery.targetingMethod = EditorHelper.EnumPopup("Targeting Method", effect.rayCastDelivery.targetingMethod);
                effect.rayCastDelivery.range = EditorGUILayout.FloatField("Max Range (0 = INF)", effect.rayCastDelivery.range);
                effect.rayCastDelivery.layerMask = EditorHelper.LayerMaskField("Layer Mask", effect.rayCastDelivery.layerMask);
                break;

            case Constants.EffectDeliveryMethod.Projectile:
                effect.projectileDelivery.targetingMethod = EditorHelper.EnumPopup("Targeting Method", effect.projectileDelivery.targetingMethod);
                effect.projectileDelivery.layerMask = EditorHelper.LayerMaskField("Layer Mask", effect.projectileDelivery.layerMask);
                effect.projectileDelivery.projectileType = EditorHelper.EnumPopup("Projectile Type", effect.projectileDelivery.projectileType);
                effect.projectileDelivery.prefabName = EditorGUILayout.TextField("Projectile Prefab Name", effect.projectileDelivery.prefabName);

                effect.projectileDelivery.kickBack = EditorGUILayout.Toggle("Kickback?", effect.projectileDelivery.kickBack);

                if (effect.projectileDelivery.kickBack) {
                    effect.projectileDelivery.kickStrength = EditorGUILayout.FloatField("Kick Strength", effect.projectileDelivery.kickStrength);
                }

                effect.projectileDelivery.error = EditorGUILayout.FloatField("Inacuraccy", effect.projectileDelivery.error);

                break;

            case Constants.EffectDeliveryMethod.Melee:
                effect.meleeDelivery.targetingMethod = EditorHelper.EnumPopup("Targeting Method", effect.meleeDelivery.targetingMethod);
                effect.meleeDelivery.layerMask = EditorHelper.LayerMaskField("Layer Mask", effect.meleeDelivery.layerMask);
                effect.meleeDelivery.prefabName = EditorGUILayout.TextField("Melee Prefab Name", effect.meleeDelivery.prefabName);
                break;

            case Constants.EffectDeliveryMethod.Rider:
                effect.riderTarget = EditorGUILayout.TextField("Host Effect Name", effect.riderTarget);

                break;
        }

        EditorGUILayout.Separator();
    }


    private Constants.SpecialAbilityEffectType DrawSpecialAbilityTypes(List<Constants.SpecialAbilityEffectType> list, int index) {
        Constants.SpecialAbilityEffectType result = EditorHelper.EnumPopup("Effect Type", list[index]);
        return result;
    }

    private SpecialAbilityData DrawSpecialAbilityData(List<SpecialAbilityData> abilityData, int index) {
        SpecialAbilityData result = EditorHelper.ObjectField<SpecialAbilityData>("Ability", abilityData[index]);
        return result;
    }

}
