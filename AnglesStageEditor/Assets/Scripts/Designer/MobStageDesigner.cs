using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using System;

public class MobStageDesigner : BaseStageDesigner
{
    [SerializeField] MobStageData stageData; // 따로 필드 만들어주기
    public MobStageData StageData { get { return stageData; } }

    [HideInInspector] public Transform easySpawnPointParent;
    [HideInInspector] public Transform normalSpawnPointParent;
    [HideInInspector] public Transform hardSpawnPointParent;

    Difficulty difficulty;
    public Difficulty Difficulty
    { get { return difficulty; } set { difficulty = value; } }

    public override void Initialize(
        Dictionary<Name, Sprite> enemyImageDictionary,
        Dictionary<Name, float> enemyScaleDictionary,
        Previewer previewerPrefab)
    {
        base.Initialize(enemyImageDictionary, enemyScaleDictionary, previewerPrefab);
        stageData = new MobStageData();
    }

    public void CreatePreview()
    {
        if (stageData == null) return;

        SpawnData[] spawnDatas;

        switch (difficulty)
        {
            case Difficulty.Easy:
                spawnDatas = stageData.easySpawnDatas;
                break;
            case Difficulty.Nomal:
                spawnDatas = stageData.normalSpawnDatas;
                break;
            case Difficulty.Hard:
                spawnDatas = stageData.hardSpawnDatas;
                break;
            default:
                spawnDatas = stageData.easySpawnDatas;
                break;
        }

        if (spawnDatas == null) return;

        RemovePreviewer();

        for (int i = 0; i < spawnDatas.Length; i++)
        {
            CreatePreviewer(spawnDatas[i]);
        }
    }

    void FillPoint(Transform pointParent, Difficulty difficulty)
    {
        int childCount = pointParent.childCount;
        SpawnData[] spawnDatas = new SpawnData[childCount];

        for (int j = 0; j < childCount; j++)
        {
            Transform point = pointParent.GetChild(j);
            spawnDatas[j] = new SpawnData(point.position, (Name)0);
        }

        stageData.AddSpawnDatas(difficulty, spawnDatas);
    }

    public void FillSpawnPoint()
    {
        FillPoint(easySpawnPointParent, Difficulty.Easy);
        FillPoint(normalSpawnPointParent, Difficulty.Nomal);
        FillPoint(hardSpawnPointParent, Difficulty.Hard);
    }

    public void RemoveSpawnPoint()
    {
        stageData.RemoveSpawnDatas(difficulty);
        RemovePreviewer();
    }

    public override void SaveData()
    {
        fileIO.SaveData(stageData, fileLocation, fileName);
    }

    public override void LoadData()
    {
        stageData = fileIO.LoadData<MobStageData>(fileLocation, fileName);
    }
}
