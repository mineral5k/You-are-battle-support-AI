using UnityEngine;

public class DefaultCharge : SkillData
{
    public DefaultCharge()
    {
        skillId = "charge";
        skillName = "기본 충전";
        skillDescription = "마나를 2 얻는다.";
        category = ActionCategory.Attack;

        manaCostType = ManaCostType.Fixed;

        fixedManaCost = 0;
        maxManaCost = 0;

        baseValue = 0;
        valuePerMana = 0;
        
        cooltime = 0;
    }

    public override void Effect(UnitState ally, UnitState enemy, int value)
    {
        ally.AddMana(2);
    }

}
