using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : Bullet
{
    public override void OnHit()
    {
        AudioManager.Instance.PlayPlayerSounds("Blade Hit");
        return;
    }
    protected override void Move()
    {
        return;
    }
    public override void m_OnBecameInvisible()
    {
        return;
    }
}
