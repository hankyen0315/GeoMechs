using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Stage2Behaviour : FastEnemyBehaviour
{
    [SerializeField]
    private float rollingSpeed;
    Transform player;
    private Vector3 moveVector;
    [SerializeField]
    private float trackTime = 8f;
    [SerializeField]
    private float trackSpeed = 10f;
    [SerializeField]
    private float trackTightness = 5f;
    public GameObject RedEyeWarning;
    public AnimationClip warningClip;


    private void Start()
    {
        player = PlayerStatsManager.Instance.gameObject.transform;
    }

    protected override void Decide()
    {
        int track = Random.Range(1, 3);
        if (track == 1)
        {
            StartCoroutine(TrackPlayer());
            return;
        }
        base.Decide();
    }

    protected override void MoveOnPath(Vector3 goal)
    {
        base.MoveOnPath(goal);
        transform.Rotate(new Vector3(0f, 0f, rollingSpeed * Time.deltaTime), Space.Self);
    }
    protected override void FaceWayPoint()
    {
        bState = BehaviourState.Move;
        return;
    }

    public override void SlowDown(float slowDownFactor)
    {
        rollingSpeed *= slowDownFactor;
        trackSpeed *= slowDownFactor;
        base.SlowDown(slowDownFactor);
    }
    public override void ResumeSpeed(float slowDownFactor)
    {
        rollingSpeed /= slowDownFactor;
        trackSpeed /= slowDownFactor;
        base.ResumeSpeed(slowDownFactor);
    }
    private IEnumerator TrackPlayer()
    {
        bState = BehaviourState.Wait;
        RedEyeWarning.SetActive(true);
        RedEyeWarning.GetComponent<Animator>().Play(warningClip.name);
        yield return new WaitForSeconds(warningClip.length);
        RedEyeWarning.SetActive(false);

        moveVector = (player.position - transform.position).normalized;
        float timer = 0f;
        while (timer <= trackTime)
        {
            timer += Time.deltaTime;
            moveVector = Vector3.RotateTowards(moveVector, player.position - transform.position, trackTightness, 0).normalized;
            transform.Rotate(new Vector3(0f, 0f, rollingSpeed * Time.deltaTime), Space.Self);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveVector, trackSpeed * Time.deltaTime);
            yield return null;
        }
        bState = BehaviourState.Decide;
    }




}
