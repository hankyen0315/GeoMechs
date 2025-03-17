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
    Vector2 wayPoint;
    MoveState moveState = MoveState.Normal;

    void Start()
    {
        SetNewDestination();
    }

    
    void Update()
    {
        if (GetComponent<EnemyStatsManager>().stopMove) return;
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
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
 