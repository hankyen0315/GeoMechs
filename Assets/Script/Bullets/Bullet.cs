using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//todo: consider if this class need to be abstract or even need this class(a interface?)
//[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IMovable, IGiveDamage
{
    public bool _active = true;
    public float Speed;
    protected MoveState moveState = MoveState.Normal;
    public float AttackPower;
    public Rigidbody2D rb { private set; get; }
    public bool PowerByExternalScript = false;

    //public bool Trace = false;
    private void Awake()
    {
        if (PowerByExternalScript) return;
        rb = GetComponent<Rigidbody2D>();
    }
    protected void FixedUpdate()
    {
        if (!PowerByExternalScript)
        {
            Move();
        }
        
        if (Offscreen())
        {
            m_OnBecameInvisible();
        }
    }
    protected virtual void Move()
    {
        float lookAngle = (transform.rotation.eulerAngles.z+90f)*Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(lookAngle), Mathf.Sin(lookAngle));
        rb.linearVelocity = direction * Speed;
    }
    protected virtual bool Offscreen()
    {
        Vector3 viewport = Camera.main.WorldToViewportPoint(transform.position);
        return viewport.x >= 1 || viewport.x <= 0 || viewport.y >= 1 || viewport.y <= 0;
    }





    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }

    public virtual void m_OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    #region interface method
    public virtual void SlowDown(float slowDownFactor)
    {
        print("slow down");
        Speed *= slowDownFactor;
        moveState = MoveState.Slow;
    }

    public virtual void ResumeSpeed(float slowDownFactor)
    {
        Speed /= slowDownFactor;
        moveState = MoveState.Normal;
    }

    public MoveState GetState()
    {
        return moveState;
    }

    public virtual void OnHit()
    {
        _active = false;
        DestroySelf();
    }

    public virtual string GetAttacker()
    {
        return gameObject.tag;
    }

    public virtual float GetAttackPower()
    {
        return AttackPower;
    }

    public virtual bool Active()
    {
        return _active;
    }
    #endregion
}
