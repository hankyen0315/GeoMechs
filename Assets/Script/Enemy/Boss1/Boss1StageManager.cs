using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//consider a more general solution in the future
public class Boss1StageManager : BossStageManager
{

    //[SerializeField]
    //private float transformRatio = 0.5f;
    [SerializeField]
    private float startHealRatio = 0.25f;
    [SerializeField]
    private Animator anim;

    protected override void RestartBoss()
    {
        foreach (CalculateEffect ce in GetComponentsInChildren<CalculateEffect>())
        {
            ce.Auto = ce.AutoState;
            ce.ExecuteAllTasks();
        }
        GetComponent<DamageDetector>().Active = true;
        anim.SetBool("idle", false);
    }

    protected override void StopBoss()
    {
        foreach (CalculateEffect ce in GetComponentsInChildren<CalculateEffect>())
        {
            ce.Auto = false;
            ce.StopAllTasks();
        }
        GetComponent<DamageDetector>().Active = false;
        anim.SetBool("idle", true);
    }

    protected override void Transformation()
    {
        anim.SetTrigger("transform");
        anim.SetInteger("stage", 2);
    }

    //private int stage = 1;
    //private float maxHealth;
    //private EnemyStatsManager statsManager;
    //bool zoomFinish = false;

    protected override void Update()
    {
        base.Update();
        if (statsManager.GetHealth() / maxHealth < startHealRatio)
        {
            anim.SetBool("startHeal", true);
        }
        else
        {
            anim.SetBool("startHeal", false);
        }
    }


}
