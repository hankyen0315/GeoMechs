using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimingBullet : Bullet
{
    private Transform player;
    private Vector3 direction;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = player.position - transform.position;
        direction = direction.normalized;
    }

    protected override void Move()
    {
        rb.linearVelocity = direction * Speed;
    }
}
