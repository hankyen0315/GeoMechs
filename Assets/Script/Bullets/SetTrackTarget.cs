using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrackTarget : MonoBehaviour
{
    public static List<GameObject> TrackTargetList = new List<GameObject>();
    public static int TargetListCapacity = 50;
    public static int TrackSameThreshold = 5;
    public List<GameObject> HittedTargetList = new List<GameObject>();
    public List<string> TrackTypeNames = new List<string>();
    private TrackBullet trackBullet;
    private void Awake()
    {
        trackBullet = GetComponentInParent<TrackBullet>();
    }


    //public Enemy_Awareness_Controller controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string typeName in TrackTypeNames)
        {
            //Type t = Type.GetType(typeName);
            if (collision.gameObject.GetComponent(typeName) as MonoBehaviour == null) continue;
            if (trackBullet.Target == null)
            {
                bool trackOtherTarget = TrackTargetList.Count <= TargetListCapacity || TrackTargetList.Count >= TrackSameThreshold;
                if (TrackTargetList.Contains(collision.gameObject) && trackOtherTarget) continue;
                if (HittedTargetList.Contains(collision.gameObject)) continue;
                //if (TrackTargetList.Count <= TargetListCapacity)
                //{
                //    trackBullet.Target = collision.gameObject.transform;
                //    break;
                //}
                TrackTargetList.Add(collision.gameObject);
                trackBullet.Target = collision.gameObject.transform;
                break;
            }
        }
        //if (collision.gameObject.GetComponent<EnemyStatsManager>() == null) return;
        //if (controller.Target == null)
        //{
        //    controller.Target = collision.gameObject.transform;
        //}
    }
    private void Update()
    {
        TrackTargetList.RemoveAll(t => t == null);
        //foreach (var target in TrackTargetList)
        //{
        //    if (target == null)
        //    {
        //        TrackTargetList.Remove()
        //    }
        //}
    }
}
