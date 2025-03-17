using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBullet : Bullet
{
    private float time = 0f;
    [SerializeField]
    private float lifeSpan;
    [SerializeField]
    private float moveDuration;
    [Tooltip("the larger the number, the steeper it decrease at the end")]
    [SerializeField]
    private int decreaseFactor;
    [SerializeField]
    [Range(0,1)]
    private float terminalSpeed;
    protected override void Move()
    {
        time += Time.fixedDeltaTime;
        if (time >= lifeSpan)
        {
            DestroySelf();
        }
        if (time >= moveDuration)
        {
            return;
        }
        base.Move();
        //alter the velocity of the rigidbody, make it moves slower over time
        rb.linearVelocity *= (1-Mathf.Pow((time/moveDuration), decreaseFactor)*(1-terminalSpeed));
    }

    public override void m_OnBecameInvisible()
    {
        return;
    }
    private void OnDestroy()
    {
        GetComponentInParent<ShieldEnemyBehaviour>()?.StartCreateShield();
    }
    public override void OnHit()
    {
        return;
    }


}
