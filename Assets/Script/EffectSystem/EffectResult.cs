using UnityEngine;


public struct EffectResult
{
    public float AttackModifier;
    public int Scatter;
    public Transform ShootPoint;


    public EffectResult(int AttackModifer, int Scatter, Transform ShootPoint) : this()
    {
        this.AttackModifier = AttackModifer;
        this.Scatter = Scatter;
        this.ShootPoint = ShootPoint;
    }
}
