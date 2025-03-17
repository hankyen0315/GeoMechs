using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBulletOverdrive : Overdrive
{
    [SerializeField]
    private float atkIntervalSpeedUp;
    [SerializeField]
    private float bulletSpeedUp;

    public override void CancelOverdriveAbility()
    {
        GetComponent<Attack>().AttackInterval *= atkIntervalSpeedUp;
        AfterBulletInitCallback = null;
    }

    public void SpeedUpBulletAndShootRate(GameObject bullet)
    {
        bullet.GetComponent<Bullet>().Speed *= bulletSpeedUp;
        //bullet.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
    }
    public override void UseOverdriveAbility()
    {
        GetComponent<Attack>().AttackInterval /= atkIntervalSpeedUp;
        AfterBulletInitCallback = SpeedUpBulletAndShootRate;
    }
}
