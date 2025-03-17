using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageDetector : DamageDetector
{
    //public bool Active = true;
    protected override bool IsHitByOthers(string tag)
    {
        return tag != "Player" && tag != "player_side";
    }
    protected override void TakeDamage(Collider2D collision)
    {
        //if (!Active) return;
        print("player collide with: " + collision.gameObject.name);
        Vector2 contactPoint = collision.ClosestPoint(new Vector2(transform.position.x, transform.position.y));
        float attackPower = 0;
        //if (collision.GetComponent<Bullet>() != null)
        //{
        //    attackPower = collision.GetComponent<Bullet>().GetAttackPower();
        //}
        //else if (collision.GetComponent<EnemyStatsManager>() != null)
        //{
        //    print("collide with enemy");
        //    if (!collision.GetComponent<EnemyStatsManager>().IsEnemyEntity) return;
        //    print("enemy entity");
        //    attackPower = collision.GetComponent<EnemyStatsManager>().CollisionDamage;
        //}
        attackPower = collision.GetComponent<IGiveDamage>().GetAttackPower();
        Instantiate(OnHitParticle, new Vector3(contactPoint.x, contactPoint.y, 0f), Quaternion.identity);
        //AudioManager.Instance.PlayPlayerSounds("Player is attacked");
        PlayerStatsManager.Instance.ChangeHealth(-attackPower);

    }
}
