using UnityEngine;

public class Burn : StatusEffect
{
    public override string Id => "Burn";
    public override string EffectName => "화상";
    public override bool IsBuff => false;

    public Burn(int damage,int duration) : base(damage, duration)
    {

    }

    public override void OnTurnEnd(UnitState owner)
    {
        owner.TakeDamage(Amount);
        Debug.Log("화상 피해" + Amount);
    }
}
