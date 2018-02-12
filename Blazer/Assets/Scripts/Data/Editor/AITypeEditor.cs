using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AIType))]
public class AITypeEditor : Editor {


    private AIType _AIType;

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        _AIType = (AIType)target;


        _AIType.AIName = EditorGUILayout.TextField("AI Name", _AIType.AIName);
        _AIType.locomotionType = EditorHelper.EnumPopup("Locomotion Type", _AIType.locomotionType);
        _AIType.attackType = EditorHelper.EnumPopup("Attack Type", _AIType.attackType);
        _AIType.initialTargetLayers = EditorHelper.DrawList("Initially targets: ", _AIType.initialTargetLayers, true, 0, true, DrawIntList);
        _AIType.turnSpeed = EditorHelper.FloatField("Turning Speed", _AIType.turnSpeed);
        _AIType.isCliffJumper = EditorGUILayout.Toggle("Jumps Off Cliffs", _AIType.isCliffJumper);
        _AIType.isWallScaler = EditorGUILayout.Toggle("Jumps Small Walls", _AIType.isWallScaler);
        _AIType.isLadderClimber = EditorGUILayout.Toggle("Climbs Up/Down Ladders", _AIType.isLadderClimber);


        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }
    private int DrawIntList(List<int> intList, int index)
    {
        int result = EditorGUILayout.IntField("Layer Number", intList[index]);
        return result;
    }
}
