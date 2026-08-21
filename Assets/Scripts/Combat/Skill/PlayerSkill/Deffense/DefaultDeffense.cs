using UnityEngine;

public class DefaultDeffense : SkillData
{
    
    public DefaultDeffense()
    {
        skillName = "기본 방어";
        skillDescription = "5의 방어도를 얻는다.";
        category = ActionCategory.Defense;

        manaCostType = ManaCostType.Fixed;

        fixedManaCost = 0;
        maxManaCost = 0;

        baseValue = 5;
        valuePerMana = 0;

        cooltime = 0;

        SEList.Add(StatusEffectType.Shield);

    }

    public override void Effect(UnitState ally, UnitState enemy, int value)
    {
        ally.AddShield(value);
    }
}
