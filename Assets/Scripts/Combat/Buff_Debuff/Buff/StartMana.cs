using UnityEngine;

public class StartMana : StatusEffect
{
    public override string Id => "StartMana";

    public override string EffectName => "시작 마나";
    public override string EffectDescription => $" {Amount}의 마나를 가지고 시작한다";
    public override bool IsBuff => true;

    public StartMana(int amount, int duration) : base(amount, duration)
    {
    }

    public override void OnTurnEnd(UnitState owner)
    {
       
    }
}
