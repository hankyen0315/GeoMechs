using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerStats
{
    public int Lives;
    public float MaxHealth;
    public float Health;
    public float MoveSpeed;
    public float RotateSpeed;
    public int Coin;
    public int MaxOverdrive;
    public int RadarUseChance;
    public int RadarUsedCount;
}
