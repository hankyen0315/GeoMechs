using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBulletOverdrive : Overdrive
{
    [SerializeField]
    private float bulletScaleUp;
    public override void CancelOverdriveAbility()
    {
        AfterBulletInitCallback = null;
    }

    public override void UseOverdriveAbility()
    {
        AfterBulletInitCallback = WidenBullet;
    }

    public void WidenBullet(GameObject bullet)
    {
        float x = bullet.transform.localScale.x;
        float y = bullet.transform.localScale.y;
        float z = bullet.transform.localScale.z;
        bullet.transform.localScale = new Vector3(x * bulletScaleUp, y, z);
    }
}
