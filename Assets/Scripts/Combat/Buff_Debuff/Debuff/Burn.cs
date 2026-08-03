using UnityEngine;

public class Burn : StatusEffect
{
    public override string Id => "Burn";
    public override string EffectName => "È­»ó";
    public override bool IsBuff => false;

    public Burn(int damage,int duration) : base(damage, duration)
    {

    }

    public override void OnTurnEnd(UnitState owner)
    {
        owner.TakeDamage(Amount);
    }
}
