using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class UpdownBehaviour : MonoBehaviour, IMovable
{
    [SerializeField]
    float speed;
    [SerializeField]
    float slowdownResistance = 1.5f;
    EnemyStatsManager health;
    MoveState moveState = MoveState.Normal;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health=GetComponent<EnemyStatsManager>();
    }

    void Update()
    {
        if (GetComponent<EnemyStatsManager>().stopMove) return;

        rb.linearVelocity = -transform.up * speed;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            health.ChangeHealth(-100, false);
        }
    }

    public void SlowDown(float slowDownFactor)
    {
        speed *= (slowDownFactor*slowdownResistance);
        moveState = MoveState.Slow;
    }

    public void ResumeSpeed(float slowDownFactor)
    {
        speed /= (slowDownFactor*slowdownResistance);
        moveState = MoveState.Normal;
    }

    public MoveState GetState()
    {
        return moveState;
    }

}
