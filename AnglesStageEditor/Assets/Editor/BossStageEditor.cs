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

    void DrawDataField(SerializedProperty property)
    {
        // Ofcourse you also want to change the list size here
        property.arraySize = EditorGUILayout.IntField("Size", property.arraySize);

        for (int i = 0; i < property.arraySize; i++)
        {
            var dialogue = property.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(dialogue, new GUIContent("Dialogue " + i), true);
        }

        // Note: You also forgot to add this
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("설정", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();
        stageDesigner.FileName = EditorGUILayout.TextField("파일 이름", stageDesigner.FileName);
        stageDesigner.FileLocation = EditorGUILayout.TextField("위치", stageDesigner.FileLocation);

        if (GUILayout.Button("저장")) stageDesigner.SaveData();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(fileToLoad, new GUIContent("FileToLoad"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("불러오기")) stageDesigner.LoadData();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("스테이지", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(bossSpawnPointParent, new GUIContent("BossSpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(mobSpawnPointParent, new GUIContent("MobSpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("스폰 위치 채우기")) 
            stageDesigner.FillSpawnPoint();

        EditorGUILayout.PropertyField(bossSpawnData, new GUIContent("BossSpawnData"));
        serializedObject.ApplyModifiedProperties();
        DrawDataField(mobSpawnDatas);
    }
}
