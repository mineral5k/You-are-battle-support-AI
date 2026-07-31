using UnityEngine;

public class CombatResolver
{
    public void ResolveTurn( UnitState ally, SelectedAction allyAction, UnitState enemy, SelectedAction enemyAction)
    {
        PrepairAction(ally, allyAction);
        PrepairAction(enemy, enemyAction);

        NonAttackPhase(ally, allyAction,enemy, enemyAction);

        AttackPhase(ally, allyAction,enemy, enemyAction);

        EndPhase(ally,allyAction, enemy,enemyAction);
    }

    public void PrepairAction(UnitState unit, SelectedAction action)
    {
        if (unit.currentMana < action.spentMana)
        {
            throw new System.InvalidOperationException("현재 마나로 사용할 수 없는 스킬입니다.");
        }

        unit.currentMana -= action.spentMana;
    }


    public void NonAttackPhase(UnitState ally, SelectedAction allyAction, UnitState enemy, SelectedAction enemyAction)
    {
        NonAttackAction(ally, allyAction, enemy);
        NonAttackAction(enemy, enemyAction,ally);
    }

    public void NonAttackAction(UnitState unit, SelectedAction action, UnitState enemy)
    {
        if (action.skill.category == ActionCategory.Attack)
            return;

        action.skill.Effect(unit,enemy);
    }

    public void AttackPhase(UnitState ally, SelectedAction allyAction, UnitState enemy, SelectedAction enemyAction)
    {
        if (allyAction.skill.category == ActionCategory.Attack && enemyAction.skill.category == ActionCategory.Attack)
        {
            if (allyAction.finalValue > enemyAction.finalValue)
            {
                allyAction.skill.Effect(ally, enemy);
            }
            else if (enemyAction.finalValue > allyAction.finalValue)
            {
                enemyAction.skill.Effect(enemy, ally);
            }
            else
            {

            }
        }

        else if (allyAction.skill.category == ActionCategory.Attack)
        {
            allyAction.skill.Effect(ally,enemy);
        }
        else if (enemyAction.skill.category == ActionCategory.Attack)
        {
            enemyAction.skill.Effect(enemy,ally);
        }
    }

    public void EndPhase(UnitState ally, SelectedAction allyAction, UnitState enemy, SelectedAction enemyAction)
    {
        foreach(SkillData skill in ally.skills)
        {
            skill.TickCooldown();
        }

        foreach (SkillData skill in enemy.skills)
        {
            skill.TickCooldown();
        }

        allyAction.skill.StartCooldown();
        enemyAction.skill.StartCooldown();
    }

}
