using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDamageDetector : DamageDetector
{
    protected override bool IsHitByOthers(string tag)
    {
        return tag != "Player" && tag != "player_side";
    }

    protected override void TakeDamage(Collider2D collision)
    {
        Vector2 contactPoint = collision.ClosestPoint(new Vector2(transform.position.x, transform.position.y));
        float attackPower = 0;
        //if (collision.GetComponent<Bullet>() != null)
        //{
        //    attackPower = collision.GetComponent<Bullet>().GetAttackPower();
        //}
        attackPower = collision.GetComponent<IGiveDamage>().GetAttackPower();
        if (OnHitParticle)
        {
            GameObject particle = Instantiate(OnHitParticle, new Vector3(contactPoint.x, contactPoint.y, 0f), Quaternion.identity);
            ParticleSystem.MainModule main = particle.GetComponent<ParticleSystem>().main;
            main.startColor = ParticleColor;
        }
        GetComponent<PartStatsManager>().ChangeHealth(-attackPower);
    }
}
