using UnityEngine;

public class AttackUp : StatusEffect
{
    public override string Id => "AttackUp";

    public override string EffectName => "위력 강화";
    public override string EffectDescription => $"공격 시 피해량이 {Amount} 증가한다";

    public override bool IsBuff => true;

    public AttackUp(int amount, int duration) : base(amount, duration)
    {
        
    }
    public override void OnTurnEnd(UnitState owner)
    {
        IsExpired = true;
    }
}
