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
    public Vector3[] pos;
    public Quaternion[] rot;
    public Vector3[] scale;
}

[Serializable]
public class PlayerData
{
    public Vector3 pos;
    public Quaternion rot;
}
