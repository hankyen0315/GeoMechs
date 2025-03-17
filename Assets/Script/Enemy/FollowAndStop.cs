using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAndStop : MonoBehaviour
{
    [Min(0f)]
    public float MinDistance = 1f;
    [Min(0f)]
    public float Velocity = 1f;

    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (!playerTransform)
            Debug.LogError("Player not found");
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyStatsManager>().stopMove) return;

        if ((transform.position - playerTransform.position).magnitude < MinDistance)
        {
            Attack();
            return;
        }
        transform.position += (playerTransform.position - transform.position).normalized * Velocity * Time.deltaTime;
    }

    void Attack()
    {

    }
}
