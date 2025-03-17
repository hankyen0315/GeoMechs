using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBulletOverdrive : Overdrive
{
    [SerializeField]
    private int HitChance;

    public override void CancelOverdriveAbility()
    {
        AfterBulletInitCallback = null;
    }

    public void AddMoreHitChance(GameObject bullet)
    {
        bullet.GetComponent<TrackBullet>().NumberOfHitBeforeDestroy = HitChance;
    }


    public override void UseOverdriveAbility()
    {
        AfterBulletInitCallback = AddMoreHitChance;
    }
}
