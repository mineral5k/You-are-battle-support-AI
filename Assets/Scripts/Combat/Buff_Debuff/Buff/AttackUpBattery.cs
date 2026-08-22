using UnityEngine;

public class AttackUpBattery : StatusEffect
{
    public override string Id => "AttackUpBattery";

    public override string EffectName => "위력 충전";
    public override string EffectDescription => $"{Amount}턴 동안 턴 시작시 공격 위력 증가 2를 얻는다";
    public override bool IsBuff => true;

    public AttackUpBattery(int amount, int duration) : base(amount, duration)
    {
    }

    public override void OnTurnEnd(UnitState owner)
    {
        owner.AddStatusEffect(new AttackUp(amount: 2, duration: 1));
        Amount--;
    }
}
