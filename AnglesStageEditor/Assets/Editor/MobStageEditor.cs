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

    SerializedProperty easySpawnDatas;
    SerializedProperty normalSpawnDatas;
    SerializedProperty hardSpawnDatas;

    protected override void OnEnable()
    {
        base.OnEnable();

        easySpawnDatas = serializedObject.FindProperty("easySpawnDatas");
        normalSpawnDatas = serializedObject.FindProperty("normalSpawnDatas");
        hardSpawnDatas = serializedObject.FindProperty("hardSpawnDatas");

        easySpawnPointParent = serializedObject.FindProperty("easySpawnPointParent");
        normalSpawnPointParent = serializedObject.FindProperty("normalSpawnPointParent");
        hardSpawnPointParent = serializedObject.FindProperty("hardSpawnPointParent");
        stageDesigner = (MobStageDesigner)target;
    }

    private void OnSceneGUI()
    {
        SpawnData[] spawnDatas = stageDesigner.GetSpawnData();
        if (spawnDatas == null) return;

        for (int i = 0; i < spawnDatas.Length; i++)
        {
            Handles.Label(new Vector3(spawnDatas[i].spawnPosition.x, spawnDatas[i].spawnPosition.y, 0), i.ToString(), labelStyle);
        }
    }

    int tabIndex = 0;
    string[] tabTexts = { Difficulty.Easy.ToString(), Difficulty.Nomal.ToString(), Difficulty.Hard.ToString() };

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

    void DrawSpawnDatas()
    {
        switch (stageDesigner.difficulty)
        {
            case Difficulty.Easy:
                DrawDataField(easySpawnDatas);
                break;
            case Difficulty.Nomal:
                DrawDataField(normalSpawnDatas);
                break;
            case Difficulty.Hard:
                DrawDataField(hardSpawnDatas);
                break;
        }
    }

    bool nowSelected = false;
    Difficulty oldDifficulty = Difficulty.Easy;

    public override void OnInspectorGUI()
    {
        if (nowSelected == false) nowSelected = true;

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

        tabIndex = GUILayout.Toolbar(tabIndex, tabTexts);
        stageDesigner.difficulty = (Difficulty)tabIndex;
        if(oldDifficulty != stageDesigner.difficulty)
        {
            oldDifficulty = stageDesigner.difficulty;
            stageDesigner.CreatePreview();
        }

        switch (stageDesigner.difficulty)
        {
            case Difficulty.Easy:
                EditorGUILayout.PropertyField(easySpawnPointParent, new GUIContent("EasySpawnPointParent"));
                serializedObject.ApplyModifiedProperties();

                if (GUILayout.Button("스폰 위치 채우기")) stageDesigner.FillSpawnPoint();
                DrawSpawnDatas();
                break;
            case Difficulty.Nomal:
                EditorGUILayout.PropertyField(normalSpawnPointParent, new GUIContent("NormalSpawnPointParent"));
                serializedObject.ApplyModifiedProperties();

                if (GUILayout.Button("스폰 위치 채우기")) stageDesigner.FillSpawnPoint();
                DrawSpawnDatas();
                break;
            case Difficulty.Hard:
                EditorGUILayout.PropertyField(hardSpawnPointParent, new GUIContent("HardSpawnPointParent"));
                serializedObject.ApplyModifiedProperties();

                if (GUILayout.Button("스폰 위치 채우기")) stageDesigner.FillSpawnPoint();
                DrawSpawnDatas();
                break;
        }

        if (nowSelected == false) stageDesigner.CreatePreview();
    }
}