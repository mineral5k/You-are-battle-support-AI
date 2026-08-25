using UnityEngine;

public class DefaultAttack : SkillData
{

    public DefaultAttack()
    {
        skillId = "attack";
        skillName = "기본 공격";
        skillDescription = "적을 5의 위력으로 공격한다";
        category = ActionCategory.Attack;

        manaCostType = ManaCostType.Fixed;

        fixedManaCost = 0;
        maxManaCost = 0;

        baseValue = 5;
        valuePerMana = 0;

        cooltime = 0;
    }

    public override void Effect(UnitState ally, UnitState enemy, int value)
    {
        enemy.TakeDamage(value);
    }

}
