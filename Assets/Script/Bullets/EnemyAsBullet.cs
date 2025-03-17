using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsBullet : Bullet
{
    
    protected override void Move()
    {
        return; // is move by the enemy bullet, temporary solution
    }
    public override void m_OnBecameInvisible()
    {
        //print("enemy as bulle became invisible");
        return;
    }

    public override void OnHit()
    {
        return;
    }

    public override void DestroySelf()
    {
        EnemyStatsManager health = GetComponent<EnemyStatsManager>();
        health.ChangeHealth(-100);
        print("enemy as bullet detroy self");
        return;
    }
}
