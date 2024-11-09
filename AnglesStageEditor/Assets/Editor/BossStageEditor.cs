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

    protected override void OnEnable()
    {
        base.OnEnable();
        bossSpawnPointParent = serializedObject.FindProperty("bossSpawnPoint");
        mobSpawnPointParent = serializedObject.FindProperty("mobSpawnPointParent");
        stageDesigner = (BossStageDesigner)target;
    }

    public override void OnInspectorGUI()
    {
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

        if (GUILayout.Button("스폰 위치 채우기")) stageDesigner.FillSpawnPoint();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("미리보기", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        if (GUILayout.Button("생성")) stageDesigner.CreatePreview();
        if (GUILayout.Button("초기화")) stageDesigner.RemovePreviewer();

        base.OnInspectorGUI();
    }
}
