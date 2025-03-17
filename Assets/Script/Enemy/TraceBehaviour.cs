using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceBehaviour : MonoBehaviour, IMovable
{


    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D _Rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private MoveState moveState = MoveState.Normal;
    
    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GetComponent<EnemyStatsManager>().stopMove) return;

        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();

    }


    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        if(_targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        _Rigidbody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _Rigidbody.linearVelocity = Vector2.zero;
        }
        else
        {
            _Rigidbody.linearVelocity = transform.up * _speed;
        }

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
