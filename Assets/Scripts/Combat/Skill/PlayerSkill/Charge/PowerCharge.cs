using UnityEngine;

public class PowerCharge : SkillData
{
   
    public PowerCharge()
    {
        skillName = "파워 차지";
        skillDescription = "4턴 동안 턴 시작시 마나 2를 얻는다. 4턴동안 <color=#FF7043>[공격 위력 증가]</color> 1을 얻는다. ";
        category = ActionCategory.Charge;

        manaCostType = ManaCostType.Fixed;

        fixedManaCost = 2;
        maxManaCost = 2;

        baseValue = 0;
        valuePerMana = 0;

        cooltime = 7;

        SEList.Add(StatusEffectType.AttackUp);
    }

    public override void Effect(UnitState ally, UnitState enemy, int value)
    {
        ally.AddStatusEffect(new ManaCharge(amount: 2, duration: 4));
        ally.AddStatusEffect(new AttackUp(amount: 1, duration: 4));
    }
}
