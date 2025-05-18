using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBulletOverdrive : Overdrive
{
    //[SerializeField]
    //private float attackUpFactor;
    //[SerializeField]
    //private Sprite powerupBullet;
    [SerializeField]
    private float ScaleUpFactor;

    public override void UseOverdriveAbility()
    {
        AfterBulletInitCallback = ScaleUpBullet;
    }

    //public void UpdateBullet(GameObject bullet)
    //{
    //    bullet.GetComponent<Bullet>().AttackPower *= attackUpFactor;
    //    bullet.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
    //}
    public void ScaleUpBullet(GameObject bullet)
    {
        bullet.transform.localScale *= ScaleUpFactor;
    }


    public override void CancelOverdriveAbility()
    {
        AfterBulletInitCallback = null;
    }
}
