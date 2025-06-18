using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class RandomMoveBehaviour : MonoBehaviour, IMovable
{
    public float speed;
    [SerializeField]
    float range;
    [SerializeField]
    float maxDistance;
    [SerializeField]
    float baseWaitTime;
    [SerializeField]
    float randomWaitOffset;


    float waitTime;
    float timer = 0f;
    Vector2 wayPoint;
    MoveState moveState = MoveState.Normal;
    EnemyStatsManager statsManager;



    void Start()
    {
        statsManager = GetComponent<EnemyStatsManager>();
        waitTime = baseWaitTime + Random.Range(-randomWaitOffset, randomWaitOffset);
        SetNewDestination();
    }

    
    void Update()
    {
        if (statsManager.stopMove) return;
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, wayPoint) < range)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                timer = 0f;
                waitTime = baseWaitTime + Random.Range(-randomWaitOffset, randomWaitOffset);
                SetNewDestination();
            }
        }
    }

    void SetNewDestination()
    {
       wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance) ,Random.Range(-maxDistance, maxDistance));
    }

    public void SlowDown(float slowDownFactor)
    {
        speed *= slowDownFactor;
        moveState = MoveState.Slow;
    }
    public void SpeedUp(float speedUpFactor)
    {
        speed *= speedUpFactor;
    }

    public void ResumeSpeed(float slowDownFactor)
    {
        speed /= slowDownFactor;
        moveState = MoveState.Normal;
    }

    public MoveState GetState()
    {
        return moveState;
    }
}
 