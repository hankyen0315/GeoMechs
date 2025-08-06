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
        GetComponentInParent<AttackManager>()?.UnregisterAttack(this);
    }



    public void AttackOnce()
    {
        if (LevelManager.State != LevelState.Fight || !Active) return;

        Overdrive od = gameObject.GetComponent<Overdrive>();
        AttackContext context = new AttackContext
        {
            Scatter = 1,
            AttackModifier = 1,
        };
        foreach (AttackPath path in attackPaths)
        {
            CalculateBuffEffect(ref context, path);

            var rotations = GetBulletRotations(path.EndPoint, context.Scatter, ScatterAngle);

            foreach (var rotation in rotations)
            {
                Transform parent = StickWithInstantiatePoint ? path.EndPoint : null;
                BulletSpawner.Instance.SpawnBullet(Bullet, path.EndPoint.position, rotation, parent, context.AttackModifier, od?.AfterBulletInitCallback);
            }
        }
    }

    private void CalculateBuffEffect(ref AttackContext context, AttackPath path)
    {
        foreach (Buff buff in path.BuffsOnPath)
        {
            if (!buff.Active) continue;
            buff.ApplyBuff(ref context);
        }
    }

    private List<Quaternion> GetBulletRotations(Transform endPoint, int scatter, float scatterAngle)
    {
        List<Quaternion> rotations = new List<Quaternion>();
        float forward = endPoint.rotation.eulerAngles.z;
        float initialAngle = (scatter % 2 == 1) ? forward : forward + scatterAngle / 2f;
        for (int i = 0; i < scatter; i++)
        {
            initialAngle += i * scatterAngle;
            rotations.Add(Quaternion.Euler(0, 0, initialAngle));
            scatterAngle = -scatterAngle; // Alternate the angle for the next bullet
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
