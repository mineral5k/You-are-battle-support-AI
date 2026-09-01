using UnityEngine;

public class SelectedAction
{
    public SkillData skill;
    public int spentMana;
    public int finalValue;

    public SelectedAction(SkillData skill, UnitState unit)
    {
        this.skill = skill;

        if (!skill.CanUse(unit.CurrentMana))
        {
            throw new System.InvalidOperationException( $"{skill.skillName}을 사용할 마나가 부족합니다.");
        }

        spentMana = skill.CalculateManaCost(unit.CurrentMana);
        int attackBuff = 0;
        if (skill.category == ActionCategory.Attack)
        {
            attackBuff = unit.AttackUp;
        }
        finalValue = skill.CalculateValue(spentMana) + attackBuff;
    }
}
