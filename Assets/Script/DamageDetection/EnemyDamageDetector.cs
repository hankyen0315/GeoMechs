using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDetector : DamageDetector
{
    GameObject new_parent;

    private void Awake()
    {
        if (onHitSound == "")
        {
            onHitSound = "Enemy is attacked";
        }
    }

    protected override bool IsHitByOthers(string tag)
    {
        return tag != "enemy_side";
    }
    protected override void TakeDamage(Collider2D collision)
    {
        Vector2 contactPoint = collision.ClosestPoint(new Vector2(transform.position.x, transform.position.y));

        float attackPower = 0;
        //if (collision.GetComponent<IGiveDamage>() != null)
        //{
        //    attackPower = collision.GetComponent<Bullet>().GetAttackPower();
        //}
        attackPower = collision.GetComponent<IGiveDamage>().GetAttackPower();
        if (attackPower <= 0) return;
        bool stillAlive = GetComponent<EnemyStatsManager>().GetHealth() + (-attackPower) > 0;
        if (OnHitParticle)
        {
            GameObject particle = Instantiate(OnHitParticle, new Vector3(contactPoint.x, contactPoint.y, 0f), Quaternion.identity);
            ParticleSystem.MainModule main = particle.GetComponent<ParticleSystem>().main;
            main.startColor = ParticleColor;
        }
        if (GetComponent<EnemyStatsManager>().IsEnemyEntity && stillAlive) 
        {
            new_parent = new GameObject("Empty_parent");
            new_parent.transform.position = gameObject.transform.position;
            gameObject.transform.parent = new_parent.transform;

            Animation vib_animation = GetComponent<Animation>();
            vib_animation.Play();
            foreach (AnimationState state in vib_animation)
            {
                state.speed = 2;
            }
        }

        //AudioManager.Instance.PlaySFX("Enemy is attacked");
        GetComponent<EnemyStatsManager>().ChangeHealth(-attackPower);
    }
}
