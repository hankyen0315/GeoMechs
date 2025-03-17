using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2StageManager : BossStageManager
{
    public Boss2Stage1Behaviour stage1Behaviour;
    public Boss2Stage2Behaviour stage2Behaviour;



    //protected  void Update()
    //{
    //    if (statsManager.GetHealth() / maxHealth < transformRatio && stage == 1)
    //    {
    //        GetComponent<Animator>().SetTrigger("transform");
    //        stage1Behaviour.enabled = false;
    //        stage2Behaviour.enabled = true;
    //        stage = 2;
    //    }
    //}

    protected override void Transformation()
    {
        GetComponent<Animator>().SetTrigger("transform");
        
    }

    protected override void StopBoss()
    {
        stage1Behaviour.enabled = false;
        foreach (CalculateEffect ce in GetComponentsInChildren<CalculateEffect>(false))
        {
            ce.Auto = false;
            ce.StopAllTasks();
        }
        GetComponent<DamageDetector>().Active = false;
    }

    protected override void RestartBoss()
    {
        foreach (CalculateEffect ce in GetComponentsInChildren<CalculateEffect>(false))
        {
            ce.Auto = ce.AutoState;
            ce.ExecuteAllTasks();
        }
        GetComponent<DamageDetector>().Active = true;
        Destroy(stage1Behaviour);
        stage2Behaviour.enabled = true;
    }
}
