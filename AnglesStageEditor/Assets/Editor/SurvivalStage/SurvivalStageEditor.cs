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
        SpawnData[] spawnDatas = stageDesigner.phaseDatas[stageDesigner.index].spawnDatas;
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
        EditorGUILayout.LabelField($"Phase {stageDesigner.index}", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("앞으로"))
        {
            if (stageDesigner.index > 0)
            {
                stageDesigner.index--;
                stageDesigner.CreatePreview();
            }
        }
        if (GUILayout.Button("뒤로"))
        {
            if (stageDesigner.index < stageDesigner.MaxPhaseIndex)
            {
                stageDesigner.index++;
                stageDesigner.CreatePreview();
            }
        }
        EditorGUILayout.EndHorizontal();

        var spawnPointProperty = spawnPointParents.GetArrayElementAtIndex(stageDesigner.index);
        EditorGUILayout.PropertyField(spawnPointProperty, new GUIContent("SpawnPointParent"));

        if (GUILayout.Button("스폰 위치 채우기")) stageDesigner.FillSpawnPoint();

        var phaseProperty = phaseDatas.GetArrayElementAtIndex(stageDesigner.index);

        EditorGUILayout.PropertyField(phaseProperty, new GUIContent("PhaseData"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
