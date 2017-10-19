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

        EditorGUILayout.Separator();

        _abilityData.recoveryType = EditorHelper.EnumPopup("Recvery Type", _abilityData.recoveryType);

        DrawRecoveryMethod(_abilityData.recoveryManager, _abilityData.recoveryType);

        EditorGUILayout.Separator();

        //List<Effect> allEffects = _abilityData.GetAllEffects();

        //for (int i = 0; i < allEffects.Count; i++) {
        //    if (allEffects[i] != null) {
        //        allEffects[i].effectType = EditorHelper.EnumPopup("Effect Delivery Method", allEffects[i].effectType);
        //    }



        //}

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


        if (entry is EffectAttack) {
            EffectAttack attackEffect = entry as EffectAttack;
            attackEffect.effectType = Constants.SpecialAbilityEffectType.AttackEffect;

            attackEffect.effectDamage = EditorGUILayout.IntField("Efect Base Damage", attackEffect.effectDamage);

            attackEffect.scaleFromBaseDamage = EditorGUILayout.Toggle("Scale From Entity base damage?", attackEffect.scaleFromBaseDamage);
            if (attackEffect.scaleFromBaseDamage) {
                attackEffect.percentOfBaseDamage = EditorHelper.PercentFloatField("Percent of base damage", attackEffect.percentOfBaseDamage);
            }
            EditorGUILayout.Separator();

            //attackEffect.range = EditorGUILayout.FloatField("Max Range", attackEffect.range);
            attackEffect.burstAttack = EditorGUILayout.Toggle("Burst?", attackEffect.burstAttack);
            EditorGUILayout.Separator();

            if (attackEffect.burstAttack) {
                attackEffect.burstInterval = EditorGUILayout.FloatField("Delay between shots", attackEffect.burstInterval);
                attackEffect.burstNumber = EditorGUILayout.IntField("Number of shots", attackEffect.burstNumber);
            }

            EditorGUILayout.Separator();

            attackEffect.penetrate = EditorGUILayout.Toggle("Penetrating?", attackEffect.penetrate);
            if (attackEffect.penetrate) {
                attackEffect.numPenetrations = EditorGUILayout.IntField("Number of Penetrations (0 = INF)", attackEffect.numPenetrations);
            }


            EditorGUILayout.Separator();
            attackEffect.fireEffectName = EditorGUILayout.TextField("Fire Effect Name", attackEffect.fireEffectName);
            attackEffect.impactEffectName = EditorGUILayout.TextField("Impact Effect Name", attackEffect.impactEffectName);


            //rayAttack.targetingMethod = EditorHelper.EnumPopup("Targeting Method", rayAttack.targetingMethod);
            ////rayAttack.layerMask = EditorGUILayout.MaskField()

            //rayAttack.layerMask = EditorHelper.LayerMaskField("Layer Mask", rayAttack.layerMask);

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

        switch (effect.deliveryMethod) {
            case Constants.EffectDeliveryMethod.Raycast:
                effect.rayCastDelivery.targetingMethod = EditorHelper.EnumPopup("Targeting Method", effect.rayCastDelivery.targetingMethod);
                effect.rayCastDelivery.range = EditorGUILayout.FloatField("Max Range (0 = INF)", effect.rayCastDelivery.range);
                effect.rayCastDelivery.layerMask = EditorHelper.LayerMaskField("Layer Mask", effect.rayCastDelivery.layerMask);
                break;
        }

        EditorGUILayout.Separator();
    }


    private Constants.SpecialAbilityEffectType DrawSpecialAbilityTypes(List<Constants.SpecialAbilityEffectType> list, int index) {
        Constants.SpecialAbilityEffectType result = EditorHelper.EnumPopup("Effect Type", list[index]);
        return result;
    }

}
