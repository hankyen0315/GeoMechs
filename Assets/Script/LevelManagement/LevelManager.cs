using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager
{
    public static int LevelProgress = 1;
    public static int TotalLevel = 3;


    private static LevelState _state = LevelState.Prepare;
    // need to think more
    public static LevelState State 
    {
        get
        {
            return _state;
        }
        set 
        {
            if (value == _state) return;
            _state = value;
        } 
    }

    public static int LastCheckPoint = 0;

    //consider making this a method in WaveManager?
    public static void ReturnToLastCheckPoint()
    {
        WaveManager.Instance.WaveCount = LastCheckPoint;
    }





    #region interface_not_finished
    private static void FightToPrepareUpdateAll()
    {
        foreach(IActiveOnFightAndPerpare obj in Resources.FindObjectsOfTypeAll(typeof(IActiveOnFightAndPerpare)))
        {
            obj.FromFightToPrepare();
        }
    }
    private static void PrepareToFightUpdateAll()
    {
        foreach (IActiveOnFightAndPerpare obj in Resources.FindObjectsOfTypeAll(typeof(IActiveOnFightAndPerpare)))
        {
            obj.FromPrepareToFight();
        }
    }
    #endregion
}

public enum LevelState
{
    Prepare, Fight
}
