using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Bullet
{
    public bool Overdriven = false;
    [SerializeField]
    private float damageDealRate;
    public GameObject Root;
    protected override void Move()
    {
        return;
    }
    public override void OnHit()
    {
        if (Overdriven)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(ReenableCollider());
        }
    }
    public override void DestroySelf()
    {
        Destroy(Root);
    }
    private IEnumerator ReenableCollider()
    {
        yield return new WaitForSeconds(damageDealRate);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public override void m_OnBecameInvisible()
    {
        return;
    }

}
