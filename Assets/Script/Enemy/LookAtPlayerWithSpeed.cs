using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerWithSpeed : MonoBehaviour
{
    private GameObject player;
    public bool reversed = false;
    public float RotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyStatsManager>().stopMove) return;
        Vector3 relativePos = player.transform.position - gameObject.transform.position;
        float angle;
        if (reversed)
        {
            angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90;
        }
        else
        {
            angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg + 90;
        }
        
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, Time.deltaTime * RotateSpeed);

    }
}
