using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject player;
    public bool reversed = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyStatsManager>().stopMove) return;
        Vector3 look = (player.transform.position - transform.position).normalized;
        if (reversed)
        {
            transform.up = -look;
            return;
        }
        transform.up = look;
    }
}
