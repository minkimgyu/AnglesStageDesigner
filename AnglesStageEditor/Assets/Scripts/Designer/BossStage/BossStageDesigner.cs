using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class BossStageDesigner : BaseStageDesigner
{
    public SpawnData bossSpawnData;
    public SpawnData[] mobSpawnDatas;

    public Transform bossSpawnPoint;
    public Transform mobSpawnPointParent;

    public void CreatePreview()
    {
        if (mobSpawnDatas == null) return;

        RemovePreviewer();
        CreatePreviewer(bossSpawnData);
        for (int i = 0; i < mobSpawnDatas.Length; i++)
        {
            CreatePreviewer(mobSpawnDatas[i]);
        }
    }

    void FillPoint(Transform bossPoint, Transform mobPointParent)
    {
        SpawnData bossSpawnData = new SpawnData(bossPoint.position, (Name)1);

        int childCount = mobPointParent.childCount;
        SpawnData[] mobSpawnDatas = new SpawnData[childCount];

        for (int j = 0; j < childCount; j++)
        {
            Transform point = mobPointParent.GetChild(j);
            mobSpawnDatas[j] = new SpawnData(point.position, (Name)1);
        }

        this.bossSpawnData = bossSpawnData;
        this.mobSpawnDatas = mobSpawnDatas;
    }

    public void FillSpawnPoint()
    {
        FillPoint(bossSpawnPoint, mobSpawnPointParent);
        CreatePreview();
    }

    public override void SaveData()
    {
        fileIO.SaveData(new BossStageData(bossSpawnData, mobSpawnDatas), fileLocation, fileName);
    }

    public override void LoadData()
    {
        BossStageData bossStageData = fileIO.LoadData<BossStageData>(fileToLoad.text);
        bossSpawnData = bossStageData.bossSpawnData;
        mobSpawnDatas = bossStageData.mobSpawnDatas;
        CreatePreview();
    }

    protected override void _OnValidate()
    {
        CreatePreview();
    }
}
