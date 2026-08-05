using UnityEngine;

public class AttackUp : StatusEffect
{
    public override string Id => "AttackUp";

    public override string EffectName => "위력 강화";

    public override bool IsBuff => true;

    public AttackUp(int amount, int duration) : base(amount, duration)
    {
        
    }
}
