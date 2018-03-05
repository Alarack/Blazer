using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StatCollectionData))]
public class StatCollectionDataEditor : Editor {

    private StatCollectionData _StatData;


    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        _StatData = (StatCollectionData)target;


        _StatData.collectionName = EditorGUILayout.TextField("Stat Template Name", _StatData.collectionName);

        EditorGUILayout.Separator();

        _StatData.stats = EditorHelper.DrawExtendedList("Stat Collection", _StatData.stats, "Stat", DrawStatDisplay);

        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }



    private StatCollectionData.StatDisplay DrawStatDisplay(StatCollectionData.StatDisplay entry) {

        entry.stat = EditorHelper.EnumPopup("Stat Type", entry.stat);

        switch (entry.stat) {
            case Constants.BaseStatType.CritChance:
                entry.maxValue = EditorHelper.PercentFloatField("Crit Chance", entry.maxValue);
                break;
            case Constants.BaseStatType.CritMultiplier:
                entry.maxValue = EditorHelper.PercentFloatField("Crit Multiplier", entry.maxValue);
                break;

            case Constants.BaseStatType.AttackSpeed:
                entry.maxValue = EditorHelper.PercentFloatField("Attack Speed", entry.maxValue);
                break;

            case Constants.BaseStatType.BaseDamage:
               entry.maxValue = EditorGUILayout.FloatField("Base Damage", entry.maxValue) * -1;
                break;

            case Constants.BaseStatType.DamageReduction:
                entry.maxValue = EditorHelper.PercentFloatField("Damage Reduction", entry.maxValue);
                break;

            default:
                entry.maxValue = EditorGUILayout.FloatField("Initial Stat Maximum", entry.maxValue);
                break;
        }




        return entry;
    }


}
