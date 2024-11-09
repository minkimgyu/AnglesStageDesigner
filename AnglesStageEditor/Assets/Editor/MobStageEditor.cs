using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MobStageDesigner))]
public class MobStageEditor : BaseStageEditor
{
    MobStageDesigner stageDesigner;

    SerializedProperty easySpawnPointParent;
    SerializedProperty normalSpawnPointParent;
    SerializedProperty hardSpawnPointParent;

    protected override void OnEnable()
    {
        base.OnEnable();

        easySpawnPointParent = serializedObject.FindProperty("easySpawnPointParent");
        normalSpawnPointParent = serializedObject.FindProperty("normalSpawnPointParent");
        hardSpawnPointParent = serializedObject.FindProperty("hardSpawnPointParent");
        stageDesigner = (MobStageDesigner)target;
    }

    private void OnSceneGUI()
    {
        SpawnData[] spawnDatas;

        switch (stageDesigner.Difficulty)
        {
            case Difficulty.Easy:
                spawnDatas = stageDesigner.StageData.easySpawnDatas;
                break;
            case Difficulty.Nomal:
                spawnDatas = stageDesigner.StageData.normalSpawnDatas;
                break;
            case Difficulty.Hard:
                spawnDatas = stageDesigner.StageData.hardSpawnDatas;
                break;
            default:
                spawnDatas = stageDesigner.StageData.easySpawnDatas;
                break;
        }

        if (spawnDatas == null) return;

        for (int i = 0; i < spawnDatas.Length; i++)
        {
            Handles.Label(new Vector3(spawnDatas[i].spawnPosition.x, spawnDatas[i].spawnPosition.y, 0), i.ToString(), labelStyle);
        }
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

        EditorGUILayout.PropertyField(easySpawnPointParent, new GUIContent("EasySpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(normalSpawnPointParent, new GUIContent("NormalSpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(hardSpawnPointParent, new GUIContent("HardSpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("���� ��ġ ä���")) stageDesigner.FillSpawnPoint();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("�̸�����", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("���̵�");
        stageDesigner.Difficulty = (Difficulty)EditorGUILayout.EnumPopup(stageDesigner.Difficulty);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("����")) stageDesigner.CreatePreview();
        if (GUILayout.Button("�ʱ�ȭ")) stageDesigner.RemovePreviewer();

        base.OnInspectorGUI();
    }
}