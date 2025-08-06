using System.Collections.Generic;

public class Buff : Part
{
    public float AttackModifier = 1;
    public int Scatter = 0;



    public virtual void ApplyBuff(ref AttackContext context)
    {
        context.AttackModifier *= AttackModifier;
        context.Scatter += Scatter;
    }

    public override Dictionary<string, string> GetPartDetail()
    {
        Dictionary<string, string> detail = new Dictionary<string, string>();
        detail.Add("Price", Price.ToString());
        detail.Add("Part Durability", GetComponent<PartStatsManager>().GetHealth().ToString());
        detail.Add("Supplement", supplement);
        detail.Add("Overdrive", OverdriveAbility);
        return detail;
    }
}
