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
        EditorGUILayout.LabelField("����", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();
        stageDesigner.FileName = EditorGUILayout.TextField("���� �̸�", stageDesigner.FileName);
        stageDesigner.FileLocation = EditorGUILayout.TextField("��ġ", stageDesigner.FileLocation);

        if (GUILayout.Button("����")) stageDesigner.SaveData();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(fileToLoad, new GUIContent("FileToLoad"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("�ҷ�����")) stageDesigner.LoadData();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("��������", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(bossSpawnPointParent, new GUIContent("BossSpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(mobSpawnPointParent, new GUIContent("MobSpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("���� ��ġ ä���")) stageDesigner.FillSpawnPoint();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("�̸�����", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        if (GUILayout.Button("����")) stageDesigner.CreatePreview();
        if (GUILayout.Button("�ʱ�ȭ")) stageDesigner.RemovePreviewer();

        base.OnInspectorGUI();
    }
}
