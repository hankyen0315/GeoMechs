using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OverdriveManager))]
public class EnemyOverdriveController : MonoBehaviour
{
    private OverdriveManager overdriveManager;


    // Start is called before the first frame update
    void Start()
    {
        overdriveManager = GetComponent<OverdriveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (overdriveManager.CanUseOverdrive)
        {
            overdriveManager.ActivateOverdrive();
        }
    }
}
