using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimingBullet : Bullet
{
    private Transform player;
    private Vector3 direction;

    public float Timeout;
    private float timer = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = player.position - transform.position;
        direction = direction.normalized;
    }

    protected override void Move()
    {
        timer += Time.deltaTime;
        if (timer >= Timeout)
        {
            DestroySelf();
            return;
        }
        rb.linearVelocity = direction * Speed;
    }
}
