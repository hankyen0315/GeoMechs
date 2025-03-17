using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBulletDamageDetector : DamageDetector
{
    protected override bool IsHitByOthers(string tag)
    {
        return tag == "enemy_side";
    }
    protected override void TakeDamage(Collider2D collision)
    {
        // or simply Destroy(gameObject)?
        GetComponent<ShieldBullet>().DestroySelf();
    }
}


