using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SurvivalStageDesigner : BaseStageDesigner
{
    /// <summary>
    /// ctrl + alt + s
    /// </summary>
    [MenuItem("StageEditor/SurvivalStageDesigner %&s")]
    public static void OpenSurvivalStageDesigner()
    {
        GameObject mobStageDesigner = new GameObject("SurvivalStageDesigner");
        mobStageDesigner.AddComponent<SurvivalStageDesigner>();
    }

    const int maxPhaseSize = 100;
    public int MaxPhaseIndex { get { return maxPhaseSize - 1; } }

    int index; // phase 인덱스
    public int Index { get { return index; } set { index = value; } } // phase 인덱스

    public int PhaseCount {  get { return index + 1; } }

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
        if (phaseData == null) return;

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
