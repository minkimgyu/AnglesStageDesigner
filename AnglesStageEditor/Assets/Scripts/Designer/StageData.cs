using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

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
    Player_Not_Used,

    YellowTriangle,
    YellowRectangle,
    YellowPentagon,
    YellowHexagon,

    RedTriangle,
    RedRectangle,
    RedPentagon,
    RedHexagon,

    OperaTriangle,
    OperaRectangle,
    OperaPentagon,
    OperaHexagon,

    Tricon,
    Rhombus,
    Pentagonic,
    Hexahorn,
    Octavia,

    Bomb,

    GreenTriangle,
    GreenRectangle,
    GreenPentagon,
    GreenHexagon,

    Hexatric
}

[Serializable]
public struct SpawnData
{
    [SerializeField] public SerializableVector2 spawnPosition; // 실질적 위치 제공
    [SerializeField] [JsonConverter(typeof(StringEnumConverter))] public Name name;

    public SpawnData(Vector3 point, Name name)
    {
        spawnPosition = new SerializableVector2(point);
        this.name = name;
    }
}