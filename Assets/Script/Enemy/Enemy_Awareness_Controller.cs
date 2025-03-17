using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Awareness_Controller : MonoBehaviour
{
    public bool AwareOfEnemy { get; private set; }

    public Vector2 DirectionToEnemy { get; private set; }

    [SerializeField]
    private float _enemyAwarenessDistance;

    public Transform Target;


    // Update is called once per frame
    void Update()
    {
        if (Target == null) 
        {
            AwareOfEnemy = false;
            return;
        }
        Vector2 selfToEnemyVector = Target.position - transform.position;
        DirectionToEnemy = selfToEnemyVector.normalized;

        if (selfToEnemyVector.magnitude <= _enemyAwarenessDistance)
        {
            AwareOfEnemy = true;
        }
        else
        {
            AwareOfEnemy = false;
        }

    }
}


