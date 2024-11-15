using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MobStageDesigner))]
public class MobStageEditor : BaseStageEditor
{
    MobStageDesigner stageDesigner;

    Dictionary<Difficulty, SerializedProperty> spawnDatas = new Dictionary<Difficulty, SerializedProperty>();
    Dictionary<Difficulty, SerializedProperty> spawnPointParent = new Dictionary<Difficulty, SerializedProperty>();

    protected override void OnEnable()
    {
        base.OnEnable();

        SerializedProperty easySpawnDatas = serializedObject.FindProperty("easySpawnDatas");
        spawnDatas.Add(Difficulty.Easy, easySpawnDatas);

        SerializedProperty normalSpawnDatas = serializedObject.FindProperty("normalSpawnDatas");
        spawnDatas.Add(Difficulty.Nomal, normalSpawnDatas);

        SerializedProperty hardSpawnDatas = serializedObject.FindProperty("hardSpawnDatas");
        spawnDatas.Add(Difficulty.Hard, hardSpawnDatas);

        SerializedProperty easySpawnPointParent = serializedObject.FindProperty("easySpawnPointParent");
        spawnPointParent.Add(Difficulty.Easy, easySpawnPointParent);

        SerializedProperty normalSpawnPointParent = serializedObject.FindProperty("normalSpawnPointParent");
        spawnPointParent.Add(Difficulty.Nomal, normalSpawnPointParent);

        SerializedProperty hardSpawnPointParent = serializedObject.FindProperty("hardSpawnPointParent");
        spawnPointParent.Add(Difficulty.Hard, hardSpawnPointParent);

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

    bool nowSelected = false;
    Difficulty oldDifficulty = Difficulty.Easy;

    public override void OnInspectorGUI()
    {
        if (nowSelected == false) nowSelected = true;

        DrawBasicInspector(stageDesigner);

        tabIndex = GUILayout.Toolbar(tabIndex, tabTexts);
        stageDesigner.difficulty = (Difficulty)tabIndex;
        if(oldDifficulty != stageDesigner.difficulty)
        {
            oldDifficulty = stageDesigner.difficulty;
            stageDesigner.CreatePreview();
        }

        EditorGUILayout.PropertyField(spawnPointParent[stageDesigner.difficulty], new GUIContent("SpawnPointParent"));

        if (GUILayout.Button("스폰 위치 채우기")) stageDesigner.FillSpawnPoint();
        EditorGUILayout.PropertyField(spawnDatas[stageDesigner.difficulty], new GUIContent("SpawnDatas"), true);

        serializedObject.ApplyModifiedProperties();

        if (nowSelected == false) stageDesigner.CreatePreview();
    }
}