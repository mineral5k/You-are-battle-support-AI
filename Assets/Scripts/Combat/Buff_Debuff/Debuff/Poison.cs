using UnityEngine;

public class Poison : StatusEffect
{
    public override string Id => "Poison";
    public override string EffectName => "독";
    public override string EffectDescription => $"턴 종료시 {Amount}의 피해를 입고, 턴 시작 시 수치가 1 증가한다.";

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
        owner.bookedDamage += Amount;
    }

}
