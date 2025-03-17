using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[RequireComponent(typeof(Effect))]
public class Attack : Part
{
    public bool StickWithInstantiatePoint = false;
    public GameObject Bullet;
    public float AttackInterval = 1f;


    private new void Start()
    {
        base.Start();
        GetComponentInParent<CalculateEffect>()?.RegisterAttack(this);
    }

    public void AttackOnce()
    {
        Effect effect = GetComponent<Effect>();
        Overdrive od = effect.gameObject.GetComponent<Overdrive>();
        if (LevelManager.State == LevelState.Fight && Active)
        {
            effect.UpdateEffect(effect.gameObject, 1, 1, null, od?.AfterBulletInitCallback);
        }
    }


    private void OnDestroy()
    {
        GetComponentInParent<CalculateEffect>()?.UnregisterAttack();
    }

    public override Dictionary<string, string> GetPartDetail()
    {
        Dictionary<string, string> detail = new Dictionary<string, string>();
        //detail.Add("Bullet Name", Bullet.name);
        detail.Add("Attack Power", Bullet.GetComponentInChildren<Bullet>().AttackPower.ToString());
        detail.Add("Bullet speed", Bullet.GetComponentInChildren<Bullet>().Speed.ToString());
        detail.Add("Attack Interval", AttackInterval.ToString()+"s");
        detail.Add("Price", Price.ToString());
        detail.Add("Part Durability", GetComponent<PartStatsManager>().GetHealth().ToString());
        detail.Add("Supplement", supplement);

        return detail;
    }
}
