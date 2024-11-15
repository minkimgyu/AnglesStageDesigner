using System;
using UnityEngine;

[Serializable]
public class MobStageData
{
    [SerializeField] public SpawnData[] easySpawnDatas;
    [SerializeField] public SpawnData[] normalSpawnDatas;
    [SerializeField] public SpawnData[] hardSpawnDatas;

    public MobStageData(
        SpawnData[] easySpawnDatas,
        SpawnData[] normalSpawnDatas,
        SpawnData[] hardSpawnDatas)
    {
        this.easySpawnDatas = easySpawnDatas;
        this.normalSpawnDatas = normalSpawnDatas;
        this.hardSpawnDatas = hardSpawnDatas;
    }
}