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
        finalValue = skill.CalculateValue(spentMana) + unit.AttackUp;
    }
}
