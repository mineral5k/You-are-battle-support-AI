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
        this.battlePresenter.Init(this);
    }

    public void SelectSkill()
    {
        ally.Selectableskills = ally.GetSelectableSkills();
        enemy.Selectableskills = enemy.GetSelectableSkills();
        SkillData allySkill = ally.Selectableskills[Random.Range(0, ally.Selectableskills.Count)];
        SkillData enemySkill = enemy.Selectableskills[Random.Range(0, enemy.Selectableskills.Count)];
        allySkillRecord.Insert(turn, allySkill);
        enemySkillRecord.Insert(turn, enemySkill);
        allyAction = new SelectedAction(allySkill,ally);
        enemyAction = new SelectedAction(enemySkill, enemy);
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

    public void EndOpenTurn()
    {
        ally.OnTurnEnd();
        enemy.OnTurnEnd();
        combatResolver.EndPhase(ally, allyAction, enemy, enemyAction);
        turn++;
    }

    public void ProcessTurn()
    {
        SelectSkill();
        StartCombat();
    }

    public void ProcessOpenTurn(SkillData skill)
    {
        SelectSkill();
        allyAction = new SelectedAction(skill, ally);
        CreatComands(ally, allyAction, enemy, enemyAction);
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

            int allyFinalValue = allySkill.finalValue;
            int enemyFinalValue = enemySkill.finalValue;

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

        battlePresenter.StartCoroutine(battlePresenter.PlayCommands(comands));
    }

}
