using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTimeOverdrive : Overdrive
{
    [SerializeField]
    private float scaleFactor;
    public override void UseOverdriveAbility()
    {
        AfterBulletInitCallback = UpdateSlowTimeAreaSize;
    }

    public void UpdateSlowTimeAreaSize(GameObject bullet)
    {
        bullet.transform.GetChild(0).transform.localScale *= scaleFactor;
    }


    public override void CancelOverdriveAbility()
    {
        AfterBulletInitCallback = null;
    }
}
