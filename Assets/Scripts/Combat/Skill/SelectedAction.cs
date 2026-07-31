using UnityEngine;

public class SelectedAction
{
    public SkillData skill;
    public int spentMana;
    public int finalValue;

    public SelectedAction(SkillData skill, int currentMana)
    {
        this.skill = skill;

        if (!skill.CanUse(currentMana))
        {
            throw new System.InvalidOperationException( $"{skill.skillName}을 사용할 마나가 부족합니다.");
        }

        spentMana = skill.CalculateManaCost(currentMana);
        finalValue = skill.CalculateValue(spentMana);
    }
}
