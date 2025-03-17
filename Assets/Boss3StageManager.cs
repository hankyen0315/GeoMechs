using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3StageManager : BossStageManager
{

    public Animator anim;
    public Overdrive newOD;


    protected override void RestartBoss()
    {
        GetComponent<EnemyStatsManager>().stopMove = false;
        foreach (CalculateEffect ce in GetComponentsInChildren<CalculateEffect>())
        {
            ce.Auto = ce.AutoState;
            ce.ExecuteAllTasks();
        }
        GetComponent<DamageDetector>().Active = true;
    }

    protected override void StopBoss()
    {
        GetComponent<EnemyStatsManager>().stopMove = true;
        foreach (CalculateEffect ce in GetComponentsInChildren<CalculateEffect>())
        {
            print("stop tasks in: " + ce.gameObject.name);
            ce.Auto = false;
            ce.StopAllTasks();
        }
        GetComponent<DamageDetector>().Active = false;
    }

    protected override void Transformation()
    {
        StartCoroutine(RotateBackAndLaughing());
    }
    private IEnumerator RotateBackAndLaughing()
    {
        float progress = 0f;
        float rotateSpeed = 2f;
        Quaternion from = gameObject.transform.rotation;
        Quaternion to = Quaternion.identity;
        while (progress <= 1f)
        {
            progress += rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(from, to, progress);
            yield return null;
        }

        anim.SetTrigger("laugh");
        AudioManager.Instance.PlaySFX("Boss3 Laugh");


    }
    //call by animation event
    public void AnimationEventAddOD()
    {
        GetComponent<OverdriveManager>().AvailableOverdrives.Add(newOD);
        newOD.Manager = GetComponent<OverdriveManager>();
        newOD.gameObject.GetComponent<Part>().ToOverdriveMaterial();
        GetComponent<OverdriveManager>().ChangeCD(12f);
    }

}
