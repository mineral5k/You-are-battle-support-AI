using UnityEngine;

public class Poison : StatusEffect
{
    public override string Id => "Poison";

    public override string EffectName => "µ¶";

    public override bool IsBuff => false;

    public Poison(int amount, int duration) : base(amount, duration)
    {
        RemainingTurns = 99;
    }

    public override void OnTurnStart(UnitState owner)
    {
        Amount++;
    }

    public override void OnTurnEnd(UnitState owner)
    {
        owner.TakeDamage(Amount);
        RemainingTurns++;
    }

}
