using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class MobStageDesigner : BaseStageDesigner
{
    //public MobStageData stageData; // 따로 필드 만들어주기
    public SpawnData[] easySpawnDatas;
    public SpawnData[] normalSpawnDatas;
    public SpawnData[] hardSpawnDatas;

    public Transform easySpawnPointParent;
    public Transform normalSpawnPointParent;
    public Transform hardSpawnPointParent;

    public Difficulty difficulty;

    protected override void _OnValidate()
    {
        CreatePreview();
    }

    public SpawnData[] GetSpawnData()
    {
        SpawnData[] spawnDatas;

        switch (difficulty)
        {
            case Difficulty.Easy:
                spawnDatas = easySpawnDatas;
                break;
            case Difficulty.Nomal:
                spawnDatas = normalSpawnDatas;
                break;
            case Difficulty.Hard:
                spawnDatas = hardSpawnDatas;
                break;
            default:
                spawnDatas = easySpawnDatas;
                break;
        }

        return spawnDatas;
    }

    public void SetSpawnData(SpawnData[] spawnDatas)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                easySpawnDatas = spawnDatas;
                break;
            case Difficulty.Nomal:
                normalSpawnDatas = spawnDatas;
                break;
            case Difficulty.Hard:
                hardSpawnDatas = spawnDatas;
                break;
            default:
                easySpawnDatas = spawnDatas;
                break;
        }
    }

    public void CreatePreview()
    {
        SpawnData[] spawnDatas = GetSpawnData();
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

        SetSpawnData(spawnDatas);
    }

    public void FillSpawnPoint()
    {
        FillPoint(easySpawnPointParent, difficulty);
        CreatePreview();
    }

    public override void SaveData()
    {
        fileIO.SaveData(
            new MobStageData(
            easySpawnDatas,
            normalSpawnDatas,
            hardSpawnDatas),

            fileLocation, 
            fileName
        );
    }

    public override void LoadData()
    {
        MobStageData mobStageData = fileIO.LoadData<MobStageData>(fileToLoad.text);
        easySpawnDatas = mobStageData.easySpawnDatas;
        normalSpawnDatas = mobStageData.normalSpawnDatas;
        hardSpawnDatas = mobStageData.hardSpawnDatas;
        CreatePreview();
    }
}
