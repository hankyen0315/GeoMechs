using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBullet : Bullet
{
    private Vector2 targetDirection;
    [SerializeField]
    private float trackRange;
    public Transform Target;
    public int NumberOfHitBeforeDestroy = 1;
    private int hitCount = 0;
    [SerializeField]
    private float rotationSpeed;
    //private SetTrackTarget targetSetter;
    [SerializeField]
    private float giveUpTime = 15f;
    private float trackTime = 0f;

    private void Start()
    {
        //targetSetter = GetComponentInChildren<SetTrackTarget>();
        if (gameObject.tag == "player_side") return;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        targetDirection = Target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        rb.SetRotation(targetRotation);
        Target = null;
    }


    protected override void Move()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }
    private void UpdateTargetDirection()
    {
        if (Target != null)
        {
            trackTime += Time.deltaTime;
            targetDirection = Target.position - transform.position;
        }
        else
        {
            trackTime = 0f;
            targetDirection = Vector2.zero;
        }
    }
    private void GiveUpCheck()
    {
        if (trackTime >= giveUpTime)
        {
            Target = null;
        }
    }
    public override void DestroySelf()
    {
        if (Target != null)
        {
            SetTrackTarget.TrackTargetList.Remove(Target.gameObject);
        }
        Destroy(gameObject);
    }

    private void RotateTowardsTarget()
    {
        if (targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        rb.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        rb.linearVelocity = transform.up * Speed;
    }
    public override void OnHit()
    {
        hitCount++;
        print("set track target: " + GetComponentInChildren<SetTrackTarget>());
        print("hitted list: " + GetComponentInChildren<SetTrackTarget>().HittedTargetList);
        print("target: " + Target);
        if (Target != null)
        {
            SetTrackTarget.TrackTargetList.Remove(Target.gameObject);
            //considering...
            //GetComponentInChildren<SetTrackTarget>().HittedTargetList.Add(Target.gameObject);
        }
        Target = null;
        if (hitCount == NumberOfHitBeforeDestroy)
        {
            _active = false;
            DestroySelf();
        }
        
    }

    //public override void SlowDown(float slowDownFactor)
    //{
    //    print("slow down");
    //    Speed *= slowDownFactor;
    //    moveState = MoveState.Slow;
    //}

    //public override void ResumeSpeed(float slowDownFactor)
    //{
    //    Speed /= slowDownFactor;
    //    moveState = MoveState.Normal;
    //}
}
