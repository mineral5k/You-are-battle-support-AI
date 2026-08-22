using UnityEditor;
using UnityEngine;

public class PoisonReady : StatusEffect
{
    public override string Id => "PoisonReady";

    public override string EffectName => "독 묻은 방어구";
    public override string EffectDescription => $"이번 턴 동안 체력피해를 입지 않았다면 턴 종료시 적에게 독을 1 부여";

    public override bool IsBuff => true;

    public PoisonReady(int amount, int duration) : base(amount, duration)
    {
        RemainingTurns = 1;
    }

    public override void OnTurnEnd(UnitState owner)
    {
        if (owner.IsDamagedThisTurn == false)
        {
            owner.target.AddStatusEffect(new Poison(amount: 1, duration: 99));
        }
        IsExpired = true;
    }
}
