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

        _abilityData.effects = EditorHelper.DrawList("Effects", _abilityData.effects, true, Constants.SpecialAbilityEffectType.None, true, DrawSpecialAbilityTypes);

        EditorGUILayout.Separator();

        for (int i = 0; i < _abilityData.effects.Count; i++) {
            ShowEffectOfType(_abilityData.effects[i], _abilityData.effectHolder);
        }


        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }



    private void ShowEffectOfType(Constants.SpecialAbilityEffectType effectType, SpecialAbilityData.EffectHolder effects) {

        switch (effectType) {
            case Constants.SpecialAbilityEffectType.RayCastAttack:
                EditorGUILayout.Separator();

                effects.rayCastAttacks = EditorHelper.DrawExtendedList("RayCast Attacks", effects.rayCastAttacks, "Raycast", DrawEffectList);

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



    private T DrawEffectList<T>(T effect) where T : Effect {

        return (T)DrawEffect(effect);
    }

    private Effect DrawEffect(Effect entry) {
        if (entry is EffectRayCastAttack) {
            EffectRayCastAttack rayAttack = entry as EffectRayCastAttack;
            rayAttack.effectType = Constants.SpecialAbilityEffectType.RayCastAttack;
            rayAttack.fireEffectName = EditorGUILayout.TextField("Fire Effect Name", rayAttack.fireEffectName);
            rayAttack.impactEffectName = EditorGUILayout.TextField("Impact Effect Name", rayAttack.impactEffectName);
            rayAttack.targetingMethod = EditorHelper.EnumPopup("Targeting Method", rayAttack.targetingMethod);
            //rayAttack.layerMask = EditorGUILayout.MaskField()

            rayAttack.layerMask = EditorHelper.LayerMaskField("Layer Mask", rayAttack.layerMask);

        }


        return entry;
    }




    private Constants.SpecialAbilityEffectType DrawSpecialAbilityTypes(List<Constants.SpecialAbilityEffectType> list, int index) {
        Constants.SpecialAbilityEffectType result = EditorHelper.EnumPopup("Effect Type", list[index]);
        return result;
    }

}
