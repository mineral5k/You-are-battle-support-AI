using UnityEngine;

public class Burn : StatusEffect
{
    public override string Id => "Burn";
    public override string EffectName => "화상";
    public override string EffectDescription => $"턴 종료시 {Amount}의 피해를 입고 수치가 1 감소한다";
    public override bool IsBuff => false;

    public Burn(int damage,int duration) : base(damage, duration)
    {
       
    }

    public override void OnTurnEnd(UnitState owner)
    {
        owner.bookedDamage += Amount;
        Amount--;
    }
}
