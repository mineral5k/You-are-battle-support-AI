using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TurnProcesser
{
    public UnitState ally;
    public UnitState enemy;
    public SelectedAction allyAction;
    public SelectedAction enemyAction;
    public CombatResolver combatResolver = new CombatResolver();
    public List<SkillData> allySkillRecord = new List<SkillData>();
    public List<SkillData> enemySkillRecord = new List<SkillData>();
    public BattlePresenter battlePresenter;
    public List<CombatCommand> comands = new List<CombatCommand>();


    public int turn = 0;

    public TurnProcesser(UnitState ally, UnitState enemy, BattlePresenter battlePresenter)
    {
        this.ally = ally;
        this.enemy = enemy;
        this.battlePresenter = battlePresenter; 
    }

    public void SelectSkill()
    {
        ally.Selectableskills = ally.GetSelectableSkills();
        enemy.Selectableskills = enemy.GetSelectableSkills();
        SkillData allySkill = ally.Selectableskills[Random.Range(0, ally.Selectableskills.Count - 1)];
        SkillData enemySkill = enemy.Selectableskills[Random.Range(0, enemy.Selectableskills.Count - 1)];
        allySkillRecord.Insert(turn, allySkill);
        enemySkillRecord.Insert(turn, enemySkill);
        allyAction = new SelectedAction(allySkill,ally.CurrentMana);
        enemyAction = new SelectedAction(enemySkill, enemy.CurrentMana);
    }

    public void StartTurn()
    {
        ally.OnTurnStart();
        enemy.OnTurnStart();
    }

    public void StartCombat()
    {
        combatResolver.ResolveTurn(ally, allyAction, enemy, enemyAction);
    }

    public void EndTurn()
    {
        ally.OnTurnEnd();
        enemy.OnTurnEnd();
        turn++;
    }

    public void ProcessTurn()
    {
        SelectSkill();
        StartCombat();
    }

    public void CreatComands(UnitState ally, SelectedAction allySkill, UnitState enemy, SelectedAction enemySkill)
    {
        comands.Clear();
        comands.Add(
            new CombatCommand
            {
                type = CombatCommandType.RevealSkill,
                user = ally,
                target = enemy,
                allyAction = allySkill,
                enemyAction = enemySkill
            });

        if (allySkill.skill.category != ActionCategory.Attack)
        {
            comands.Add(
            new CombatCommand
            {
                type = CombatCommandType.ExecuteSkill,
                user = ally,
                target = enemy,
                allyAction = allySkill,
                enemyAction = enemySkill
            });

            comands.Add(
            new CombatCommand
            {
                type = CombatCommandType.ExecuteSkill,
                user = enemy,
                target = ally,
                allyAction = enemySkill,
                enemyAction = allySkill
            });
        }

        else if (enemySkill.skill.category != ActionCategory.Attack)
        {
            comands.Add(
            new CombatCommand
            {
                type = CombatCommandType.ExecuteSkill,
                user = enemy,
                target = ally,
                allyAction = enemySkill,
                enemyAction = allySkill
            });

            comands.Add(
            new CombatCommand
            {
                type = CombatCommandType.ExecuteSkill,
                user = ally,
                target = enemy,
                allyAction = allySkill,
                enemyAction = enemySkill
            });
        }

        else if (enemySkill.skill.category == ActionCategory.Attack)
        {
            comands.Add(
            new CombatCommand
            {
                type = CombatCommandType.RevealClash,
                user = ally,
                target = enemy,
                allyAction = allySkill,
                enemyAction = enemySkill
            });

            int allyFinalValue = allySkill.finalValue + ally.AttackUp;
            int enemyFinalValue = enemySkill.finalValue + enemy.AttackUp;

            if (allyFinalValue > enemyFinalValue)
            {
                comands.Add(
                    new CombatCommand
                    {
                        type = CombatCommandType.ExecuteSkill,
                        user = ally,
                        target = enemy,
                        allyAction = allySkill,
                        enemyAction = enemySkill
                    });
            }

            else if (allyFinalValue < enemyFinalValue)
            {
                comands.Add(
                    new CombatCommand
                    {
                        type = CombatCommandType.ExecuteSkill,
                        user = enemy,
                        target = ally,
                        allyAction = enemySkill,
                        enemyAction = allySkill
                    });
            }
        }

    }

}
