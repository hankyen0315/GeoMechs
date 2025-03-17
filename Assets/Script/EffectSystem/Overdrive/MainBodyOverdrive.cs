using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBodyOverdrive : Overdrive
{
    public MainBody mainBody;

    public override void CancelOverdriveAbility()
    {
        mainBody.ExitOverdriveMode();
    }

    public override void UseOverdriveAbility()
    {
        mainBody.EnterOverdriveMode();
    }
}
