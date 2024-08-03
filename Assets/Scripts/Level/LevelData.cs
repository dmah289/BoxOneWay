using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level Data")]
public class LevelData : ScriptableObject
{
    public int levelIdx;
    public PlayerData playerData;
    public PlatformData[] platformData;


    public static class Database
    {
        public static LevelData GetCurrentLevelByIdx(int idx)
            => Resources.Load<LevelData>($"LevelData/Level {idx}");
    }
}

[Serializable]
public class PlatformData
{
    public Vector2[] positions;
    public Vector2[] rotations;
    public Vector2[] scales;
}

[Serializable]
public class PlayerData
{
    public Vector2 position;
    public Vector2 rotation;
    public int Direction
    {
        get => rotation.x == 0 ? 1 : -1;
    }
}
