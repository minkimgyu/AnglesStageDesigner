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

    private void OnSceneGUI()
    {
        SpawnData[] spawnDatas = stageDesigner.mobSpawnDatas;
        if (spawnDatas == null) return;

        for (int i = 0; i < spawnDatas.Length; i++)
        {
            Handles.Label(new Vector3(spawnDatas[i].spawnPosition.x, spawnDatas[i].spawnPosition.y, 0), i.ToString(), labelStyle);
        }
    }

    public override void OnInspectorGUI()
    {
        DrawBasicInspector(stageDesigner);

        EditorGUILayout.PropertyField(bossSpawnPointParent, new GUIContent("Boss SpawnPoint Parent"));
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(mobSpawnPointParent, new GUIContent("Mob SpawnPoint Parent"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Fill Spawn Point")) 
            stageDesigner.FillSpawnPoint();

        EditorGUILayout.PropertyField(bossSpawnData, new GUIContent("Boss Spawn Data"));
        EditorGUILayout.PropertyField(mobSpawnDatas, new GUIContent("Boss Spawn Data"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
