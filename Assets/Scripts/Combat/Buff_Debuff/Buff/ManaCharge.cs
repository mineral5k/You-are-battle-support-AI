using UnityEngine;

public class ManaCharge : StatusEffect
{
    public override string Id => "ManaCharge";

    public override string EffectName => "마나 충전";
    public override string EffectDescription => $"턴 종료시 마나를 {Amount} 회복한다.";
    public override bool IsBuff => true;

    public ManaCharge(int amount, int duration) : base(amount, duration)
    {
    }

    public override void OnTurnEnd(UnitState owner)
    {
        owner.AddMana(Amount);
        IsExpired = true;
    }
}
