using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Stage1Behaviour : FastEnemyBehaviour
{
    [SerializeField]
    private float swingSpeed;
    [SerializeField]
    private float swingDistance;
    // 1 stand for charge, 2 stand for swing attack
    private int action;
    public GameObject RedEyeWarning;
    public AnimationClip warningClip;
    [SerializeField]
    private float attackWarningTime = 0.5f;

    [SerializeField]
    private float chargeSpeed;

    [SerializeField]
    private OverdriveManager odManager;
    private Transform player;
    private void Start()
    {
        player = PlayerStatsManager.Instance.gameObject.transform;
    }

    //protected override void Decide()
    //{
    //    action = Random.Range(1,3);
    //    base.Decide();
    //}

    protected override void OnLastPoint()
    {
        bState = BehaviourState.Wait;
        StartCoroutine(ChargeAttack());
        //if (action == 1)
        //{
            
        //}
        //if (action == 2)
        //{
        //    StartCoroutine(SwingFullCircle());
        //}
    }
    protected override void OnWayPoint()
    {
        if (Vector3.Distance(transform.position, player.position) < swingDistance){
            bState = BehaviourState.Wait;
            StartCoroutine(SwingFullCircle());
        }
        else
        {
            base.OnWayPoint();
        }
    }



    private IEnumerator SwingFullCircle()
    {
        odManager.ActivateOverdrive();
        RedEyeWarning.SetActive(true);

        //RedEyeWarning.GetComponent<Animator>().Play("red_eye_warning");
        RedEyeWarning.GetComponent<Animator>().Play(warningClip.name);
        yield return new WaitForSeconds(warningClip.length);
        RedEyeWarning.SetActive(false);

        float progress = 0f;
        float from = transform.rotation.z;
        print("from:" + from.ToString());

        float to = transform.rotation.z + 360f;
        print("to: " + to.ToString());
        while (progress <= 1)
        {
            print("rotating progress: " + progress.ToString());
            progress = progress + Time.deltaTime * swingSpeed;
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(from, to, progress));
            yield return null;
        }
        base.OnWayPoint();
    }

    private IEnumerator ChargeAttack()
    {
        Transform player = PlayerStatsManager.Instance.gameObject.transform;
        Vector3 relativePos = player.position - transform.position;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90;
        Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion from = transform.rotation;
        Vector3 goal = player.position;

        // rotate
        float progress = 0f;
        while (progress <= 1f)
        {
            progress = progress + Time.deltaTime * rotateSpeed;
            transform.rotation = Quaternion.Slerp(from, to, progress);
            yield return null;
        }

        // warning
        RedEyeWarning.SetActive(true);
        //RedEyeWarning.GetComponent<Animator>().Play("red_eye_warning");
        RedEyeWarning.GetComponent<Animator>().Play(warningClip.name);
        yield return new WaitForSeconds(warningClip.length);
        //yield return new WaitForSeconds(attackWarningTime);
        RedEyeWarning.SetActive(false);

        //charge
        progress = 0f;
        Vector3 start = transform.position;
        while (progress <= 1f)
        {
            progress = progress + Time.deltaTime * chargeSpeed;
            print("charge progress: " + progress.ToString());
            print("goal position: " + goal.ToString());
            print("my position: " + transform.position.ToString());
            print("to goal distance: " + Vector3.Distance(goal, transform.position).ToString());
            transform.position = Vector3.Lerp(start, goal, progress);
            yield return null;
        }

        base.OnLastPoint();
    }

    public override void SlowDown(float slowDownFactor)
    {
        chargeSpeed *= slowDownFactor;
        swingSpeed *= slowDownFactor;
        base.SlowDown(slowDownFactor);
    }
    public override void ResumeSpeed(float slowDownFactor)
    {
        chargeSpeed /= slowDownFactor;
        swingSpeed /= slowDownFactor;
        base.ResumeSpeed(slowDownFactor);
    }



}
