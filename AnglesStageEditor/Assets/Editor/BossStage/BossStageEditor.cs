using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BossStageDesigner))]
public class BossStageEditor : BaseStageEditor
{
    BossStageDesigner stageDesigner;

    SerializedProperty bossSpawnPointParent;
    SerializedProperty mobSpawnPointParent;

    SerializedProperty bossSpawnData;
    SerializedProperty mobSpawnDatas;

    protected override void OnEnable()
    {
        base.OnEnable();

        bossSpawnData = serializedObject.FindProperty("bossSpawnData");
        mobSpawnDatas = serializedObject.FindProperty("mobSpawnDatas");

        bossSpawnPointParent = serializedObject.FindProperty("bossSpawnPoint");
        mobSpawnPointParent = serializedObject.FindProperty("mobSpawnPointParent");
        stageDesigner = (BossStageDesigner)target;
    }

    public override void OnInspectorGUI()
    {
        DrawBasicInspector(stageDesigner);

        EditorGUILayout.PropertyField(bossSpawnPointParent, new GUIContent("BossSpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(mobSpawnPointParent, new GUIContent("MobSpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Fill Spawn Point")) 
            stageDesigner.FillSpawnPoint();

        EditorGUILayout.PropertyField(bossSpawnData, new GUIContent("BossSpawnData"));
        EditorGUILayout.PropertyField(mobSpawnDatas, new GUIContent("BossSpawnData"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
