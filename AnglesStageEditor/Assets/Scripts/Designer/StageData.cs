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