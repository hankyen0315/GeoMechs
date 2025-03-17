using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Trace_Behaviour : MonoBehaviour, IMovable
{


    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D _Rigidbody;
    private Enemy_Awareness_Controller _Enemy_Awareness_Controller;
    private Vector2 _targetFirection;
    private MoveState moveState = MoveState.Normal;

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _Enemy_Awareness_Controller = GetComponent<Enemy_Awareness_Controller>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();

    }


    private void UpdateTargetDirection()
    {
        if (_Enemy_Awareness_Controller.AwareOfEnemy)
        {
            _targetFirection = _Enemy_Awareness_Controller.DirectionToEnemy;
        }
        else
        {
            _targetFirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        if (_targetFirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetFirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        _Rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        //if (_targetFirection == Vector2.zero)
        //{
        //    _Rigidbody.velocity = Vector2.up ;
        //}
        //else
        //{
        //    _Rigidbody.velocity = transform.up * _speed;
        //}
        _Rigidbody.linearVelocity = transform.up * _speed;
    }

    public void SlowDown(float slowDownFactor)
    {
        _speed *= slowDownFactor;
        moveState = MoveState.Slow;
    }

    public void ResumeSpeed(float slowDownFactor)
    {
        _speed /= slowDownFactor;
        moveState = MoveState.Normal;
    }

    public MoveState GetState()
    {
        return moveState;
    }
}
