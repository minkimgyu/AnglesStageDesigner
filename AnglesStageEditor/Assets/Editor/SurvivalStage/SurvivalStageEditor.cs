using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SurvivalStageDesigner))]
public class SurvivalStageEditor : BaseStageEditor
{
    SurvivalStageDesigner stageDesigner;

    SerializedProperty phaseDatas;
    SerializedProperty spawnPointParents;

    protected override void OnEnable()
    {
        base.OnEnable();

        stageDesigner = (SurvivalStageDesigner)target;

        phaseDatas = serializedObject.FindProperty("phaseDatas");
        spawnPointParents = serializedObject.FindProperty("spawnPointParents");
    }

    private void OnSceneGUI()
    {
        SpawnData[] spawnDatas = stageDesigner.phaseDatas[stageDesigner.Index].spawnDatas;
        if (spawnDatas == null) return;

        for (int i = 0; i < spawnDatas.Length; i++)
        {
            Handles.Label(new Vector3(spawnDatas[i].spawnPosition.x, spawnDatas[i].spawnPosition.y, 0), i.ToString(), labelStyle);
        }
    }

    public override void OnInspectorGUI()
    {
        DrawBasicInspector(stageDesigner);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField($"Phase {stageDesigner.PhaseCount}", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Foward"))
        {
            if (stageDesigner.Index > 0)
            {
                stageDesigner.Index--;
                stageDesigner.CreatePreview();
            }
        }
        if (GUILayout.Button("Backward"))
        {
            if (stageDesigner.Index < stageDesigner.MaxPhaseIndex)
            {
                stageDesigner.Index++;
                stageDesigner.CreatePreview();
            }
        }
        EditorGUILayout.EndHorizontal();

        var spawnPointProperty = spawnPointParents.GetArrayElementAtIndex(stageDesigner.Index);
        EditorGUILayout.PropertyField(spawnPointProperty, new GUIContent("Spawn Point Parent"));

        if (GUILayout.Button("Fill Spawn Point")) stageDesigner.FillSpawnPoint();

        var phaseProperty = phaseDatas.GetArrayElementAtIndex(stageDesigner.Index);

        EditorGUILayout.PropertyField(phaseProperty, new GUIContent("Phase Data"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
