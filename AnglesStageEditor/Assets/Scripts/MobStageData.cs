using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Difficulty
{
    Easy,
    Nomal,
    Hard,
}

[Serializable]
public enum Name
{
    YellowTriangle,
    YellowRectangle,
    YellowPentagon,
    YellowHexagon,

    RedTriangle,
    RedRectangle,
    RedPentagon,
    RedHexagon,

    Tricon,
    Rhombus,
    Pentagonic
}

[Serializable]
public class SpawnData
{
    [SerializeField] public SerializableVector2 spawnPosition; // 실질적 위치 제공
    [SerializeField] public Name name;

    public SpawnData()
    {
    }

    public SpawnData(Vector3 point, Name name)
    {
        spawnPosition = new SerializableVector2(point);
        this.name = name;
    }
}

[Serializable]
public class BossStageData
{
    [SerializeField] public SpawnData bosssSpawnData;
    [SerializeField] public SpawnData[] mobSpawnDatas;

    public void AddSpawnDatas(SpawnData bosssSpawnData, SpawnData[] mobSpawnDatas)
    {
        this.bosssSpawnData = bosssSpawnData;
        this.mobSpawnDatas = mobSpawnDatas;
    }

    public void RemoveSpawnDatas()
    {
        bosssSpawnData = null;
        mobSpawnDatas = null;
    }
}

[Serializable]
public class MobStageData
{
    [SerializeField] public SpawnData[] easySpawnDatas;
    [SerializeField] public SpawnData[] normalSpawnDatas;
    [SerializeField] public SpawnData[] hardSpawnDatas;

    public void AddSpawnDatas(Difficulty difficulty, SpawnData[] spawnDatas)
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
                break;
        }
    }

    public void RemoveSpawnDatas(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                easySpawnDatas = null;
                break;
            case Difficulty.Nomal:
                normalSpawnDatas = null;
                break;
            case Difficulty.Hard:
                hardSpawnDatas = null;
                break;
            default:
                break;
        }
    }
}