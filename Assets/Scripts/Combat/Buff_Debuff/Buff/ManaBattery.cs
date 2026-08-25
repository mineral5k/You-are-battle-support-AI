using UnityEngine;

public class ManaBattery : StatusEffect
{
    public override string Id => "ManaBattery";

    public override string EffectName => "마나 충전";
    public override string EffectDescription => $"{Amount}턴 동안 턴 종료시 마나를 2 얻는다";
    public override bool IsBuff => true;

    public ManaBattery(int amount, int duration) : base(amount, duration)
    {

    }

    public override void OnTurnEnd(UnitState owner)
    {
        owner.AddMana(2);
        Amount--;
    }
}
