using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class BossStageDesigner : BaseStageDesigner
{
    [SerializeField] BossStageData stageData; // 따로 필드 만들어주기
    public BossStageData StageData { get { return stageData; } }

    [HideInInspector] public Transform bossSpawnPoint;
    [HideInInspector] public Transform mobSpawnPointParent;

    public override void Initialize(Dictionary<Name, Sprite> enemyImageDictionary, Dictionary<Name, float> enemyScaleDictionary, Previewer previewerPrefab)
    {
        base.Initialize(enemyImageDictionary, enemyScaleDictionary, previewerPrefab);
        stageData = new BossStageData();
    }

    public void CreatePreview()
    {
        if (stageData == null) return;

        RemovePreviewer();

        CreatePreviewer(stageData.bosssSpawnData);
        for (int i = 0; i < stageData.mobSpawnDatas.Length; i++)
        {
            CreatePreviewer(stageData.mobSpawnDatas[i]);
        }
    }

    void FillPoint(Transform bossPoint, Transform mobPointParent)
    {
        SpawnData mobSpawnData = new SpawnData(bossPoint.position, (Name)0);

        int childCount = mobPointParent.childCount;
        SpawnData[] mobSpawnDatas = new SpawnData[childCount];

        for (int j = 0; j < childCount; j++)
        {
            Transform point = mobPointParent.GetChild(j);
            mobSpawnDatas[j] = new SpawnData(point.position, (Name)0);
        }

        stageData.AddSpawnDatas(mobSpawnData, mobSpawnDatas);
    }

    public void FillSpawnPoint()
    {
        FillPoint(bossSpawnPoint, mobSpawnPointParent);
    }

    public void RemoveSpawnPoint()
    {
        stageData.RemoveSpawnDatas();
        RemovePreviewer();
    }

    public override void SaveData()
    {
        fileIO.SaveData(stageData, fileLocation, fileName);
    }

    public override void LoadData()
    {
        stageData = fileIO.LoadData<BossStageData>(fileLocation, fileName);
    }
}
