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

        EditorGUILayout.PropertyField(easySpawnPointParent, new GUIContent("EasySpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(normalSpawnPointParent, new GUIContent("NormalSpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(hardSpawnPointParent, new GUIContent("HardSpawnPointParent"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("스폰 위치 채우기")) stageDesigner.FillSpawnPoint();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("미리보기", labelStyle, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("난이도");
        stageDesigner.Difficulty = (Difficulty)EditorGUILayout.EnumPopup(stageDesigner.Difficulty);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("생성")) stageDesigner.CreatePreview();
        if (GUILayout.Button("초기화")) stageDesigner.RemovePreviewer();

        base.OnInspectorGUI();
    }
}