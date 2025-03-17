using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterOverdrive : Overdrive
{
    [SerializeField]
    private int addedScatter;
    public override void UseOverdriveAbility()
    {
        GetComponent<Buff>().Scatter += addedScatter;
    }

    public override void CancelOverdriveAbility()
    {
        GetComponent<Buff>().Scatter -= addedScatter;
    }
}
