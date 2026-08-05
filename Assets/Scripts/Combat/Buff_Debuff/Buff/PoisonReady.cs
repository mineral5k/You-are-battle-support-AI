using UnityEngine;

public class PoisonReady : StatusEffect
{
    public override string Id => "PoisonReady";

    public override string EffectName => "독 묻은 방어구";

    public override bool IsBuff => true;

    public PoisonReady(int amount, int duration) : base(amount, duration)
    {
        RemainingTurns = 1;
    }

    public override void OnTurnEnd(UnitState owner)
    {
        owner.target.AddStatusEffect(new Poison(amount: 1, duration: 99));
    }
}
