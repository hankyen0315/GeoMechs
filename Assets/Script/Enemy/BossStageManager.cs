using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStatsManager))]
public abstract class BossStageManager : MonoBehaviour
{
    [SerializeField]
    protected List<float> transformRatioPoints = new List<float>();
    [SerializeField]
    protected float transformTime = 1f;
    //[SerializeField]
    //protected float transformRatio = 0.5f;
    //[SerializeField]
    //private float startHealRatio = 0.25f;
    protected int stage = 1;
    protected float maxHealth;
    protected EnemyStatsManager statsManager;
    private bool finishZoom = true;
    CameraMagnify cameraControl;
    [SerializeField]
    protected float zoomSpeed;
    [SerializeField]
    protected float targetZoomSize;
    CutScene cutScene;
    private void Start()
    {
        statsManager = GetComponent<EnemyStatsManager>();
        maxHealth = statsManager.GetHealth();
        cameraControl = FindAnyObjectByType<CameraMagnify>();
        cutScene = FindAnyObjectByType<CutScene>();
    }

    protected virtual void Update()
    {
        bool needTransform = stage <= transformRatioPoints.Count && statsManager.GetHealth() / maxHealth < transformRatioPoints[stage - 1];
        if (needTransform && finishZoom)
        {
            print("start transfrom");
            ////zoomFinish = FindObjectOfType<CameraMagnify>().GoToEnemy(transform.position);
            //GetComponent<Animator>().SetTrigger("transform");
            //GetComponent<Animator>().SetInteger("stage", 2);
            stage++;

            StopBoss();
            cutScene.StopPlayer();
            //FindAnyObjectByType<CameraMagnify>().GoToEnemy(transform.position);
            finishZoom = false;
            StartCoroutine(ZoomToBoss());
            
            
        }
        //if (statsManager.GetHealth() / maxHealth < startHealRatio)
        //{
        //    GetComponent<Animator>().SetBool("startHeal", true);
        //}
        //else
        //{
        //    GetComponent<Animator>().SetBool("startHeal", false);
        //}
    }
    protected virtual IEnumerator ZoomToBoss()
    {
        while (!finishZoom)
        {
            finishZoom = cameraControl.GoToEnemy(transform.position, targetZoomSize);
            yield return null;
        }
        Transformation();
        yield return new WaitForSeconds(transformTime);
        finishZoom = false;
        while (!finishZoom)
        {
            finishZoom = cameraControl.GoToCenter();
            yield return null;
        }
        cutScene.RestartPlayer();
        RestartBoss();
        //transformAction();
    }
    //private void OnZoomFinish()
    //{
    //    Transformation();
    //    restartplayer
    //}
    protected abstract void Transformation();
    protected abstract void StopBoss();
    protected abstract void RestartBoss();
}
