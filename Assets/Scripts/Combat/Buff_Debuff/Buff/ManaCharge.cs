using UnityEngine;

public class ManaCharge : StatusEffect
{
    public override string Id => "ManaCharge";

    public override string EffectName => "마나 충전";

    public override bool IsBuff => true;

    public ManaCharge(int amount, int duration) : base(amount, duration)
    {
    }

    public override void OnTurnStart(UnitState owner)
    {
        owner.AddMana(Amount);
    }
}
