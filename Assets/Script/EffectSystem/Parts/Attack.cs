using System.Collections.Generic;
using UnityEngine;


public class Attack : Part
{
    public bool StickWithInstantiatePoint = false;
    public GameObject Bullet;

    public float AttackInterval = 1f;
    public float ScatterAngle = 25f;

    private List<AttackPath> attackPaths = new List<AttackPath>();

    private new void Start()
    {
        base.Start();
        GetComponentInParent<AttackManager>()?.RegisterAttack(this);
    }
    private void OnDestroy()
    {
        GetComponentInParent<AttackManager>()?.UnregisterAttack();
    }



    public void AttackOnce()
    {
        if (LevelManager.State != LevelState.Fight || !Active) return;

        Overdrive od = gameObject.GetComponent<Overdrive>();
        
        foreach (AttackPath path in attackPaths)
        {
            float attackModifier = 1;
            int scatter = 1;
            foreach (Buff buff in path.BuffsOnPath)
            {
                if (!buff.Active) continue;
                attackModifier *= buff.AttackModifier;
                scatter += buff.Scatter;
            }
            var rotations = GetBulletRotations(path.EndPoint, scatter, ScatterAngle);

            foreach (var rotation in rotations)
            {
                Transform parent = StickWithInstantiatePoint ? path.EndPoint : null;
                BulletSpawner.Instance.SpawnBullet(Bullet, path.EndPoint.position, rotation, parent, attackModifier, od?.AfterBulletInitCallback);
            }
        }
    }

    private List<Quaternion> GetBulletRotations(Transform endPoint, int scatter, float scatterAngle)
    {
        List<Quaternion> rotations = new List<Quaternion>();
        float negValue = -scatterAngle;
        float forward = endPoint.rotation.eulerAngles.z;
        float initialAngle = (scatter % 2 == 1) ? forward : forward - negValue / 2f;
        for (int i = 0; i < scatter; i++)
        {
            negValue = -negValue;
            initialAngle += i * negValue;
            rotations.Add(Quaternion.Euler(0, 0, initialAngle));
        }
        return rotations;
    }

    public void RebuildAttackPaths()
    {
        attackPaths = AttackPathBuilder.BuildAllAttackPaths(transform);
    }




    public override Dictionary<string, string> GetPartDetail()
    {
        Dictionary<string, string> detail = new Dictionary<string, string>();
        detail.Add("Attack Power", Bullet.GetComponentInChildren<Bullet>().AttackPower.ToString());
        detail.Add("Bullet Speed", Bullet.GetComponentInChildren<Bullet>().Speed.ToString());
        detail.Add("Attack Interval", AttackInterval.ToString()+"s");
        detail.Add("Price", Price.ToString());
        detail.Add("Part Durability", GetComponent<PartStatsManager>().GetHealth().ToString());
        detail.Add("Supplement", supplement);
        detail.Add("Overdrive", OverdriveAbility);

        return detail;
    }
}
