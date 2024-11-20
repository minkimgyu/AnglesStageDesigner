using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalStageDesigner : BaseStageDesigner
{
    const int maxPhaseSize = 50;
    public int MaxPhaseIndex { get { return maxPhaseSize - 1; } }

    public int index; // phase 인덱스
    public PhaseData[] phaseDatas = new PhaseData[maxPhaseSize];
    public Transform[] spawnPointParents = new Transform[maxPhaseSize];

    public override void LoadData()
    {
        SurvivalStageData mobStageData = fileIO.LoadData<SurvivalStageData>(fileToLoad.text);
        phaseDatas = mobStageData.phaseDatas;
        CreatePreview();
    }

    public override void SaveData()
    {
        fileIO.SaveData(
            new SurvivalStageData(phaseDatas),
            fileLocation,
            fileName
        );
    }

    public void FillSpawnPoint()
    {
        // 기존 값이 있다면 Name은 남기고 Pos만 변경해주기
        Transform spawnPointParent = spawnPointParents[index];

        int childCount = spawnPointParent.childCount;
        SpawnData[] spawnDatas = new SpawnData[childCount];
        for (int j = 0; j < childCount; j++)
        {
            Transform point = spawnPointParent.GetChild(j);
            spawnDatas[j] = new SpawnData(point.position, (Name)1);
        }

        phaseDatas[index] = new PhaseData(0, spawnDatas);
        CreatePreview();
    }

    public void CreatePreview()
    {
        PhaseData phaseData = phaseDatas[index];
        SpawnData[] spawnDatas = phaseData.spawnDatas;
        if (spawnDatas == null) return;

        RemovePreviewer();

        for (int i = 0; i < spawnDatas.Length; i++)
        {
            CreatePreviewer(spawnDatas[i]);
        }
    }

    protected override void _OnValidate()
    {
        CreatePreview();
    }
}
