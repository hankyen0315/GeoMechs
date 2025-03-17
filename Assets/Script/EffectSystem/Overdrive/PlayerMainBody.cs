using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainBody : MainBody
{
    //[SerializeField]
    //private float speedUpFactor = 1.25f;
    public override void EnterOverdriveMode()
    {
        PlayerStatsManager.Instance.SpeedUp(speedUpFactor);
    }

    public override void ExitOverdriveMode()
    {
        PlayerStatsManager.Instance.ResumeSpeed(speedUpFactor);
    }

    public override Dictionary<string, string> GetPartDetail()
    {
        return null;
    }
}
