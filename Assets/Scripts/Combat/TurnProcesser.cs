using NUnit.Framework;
using System;
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
    public bool isProcessing = false;
    public Action turnPanelRefresh;

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
        SkillData allySkill;
        SkillData enemySkill;
        while (true)
        {
            allySkill = ally.Selectableskills[UnityEngine.Random.Range(0, ally.Selectableskills.Count)];
            if ( !(allySkill.category == ActionCategory.Charge && ally.CurrentMana >= 9) ) break;
        }

        while (true)
        {
            enemySkill = enemy.Selectableskills[UnityEngine.Random.Range(0, enemy.Selectableskills.Count)];
            if (!(enemySkill.category == ActionCategory.Charge && enemy.CurrentMana >= 9)) break;
        }

        allySkillRecord.Add(allySkill);
        enemySkillRecord.Add(enemySkill);
        allyAction = new SelectedAction(allySkill,ally);
        enemyAction = new SelectedAction(enemySkill, enemy);
    }

    public void StartTurn()
    {
        turn++;
        ally.OnTurnStart();
        enemy.OnTurnStart();
        isProcessing = false;
        turnPanelRefresh?.Invoke();
        Debug.Log($"{turn}ео");
    }

    public void StartCombat()
    {
        combatResolver.ResolveTurn(ally, allyAction, enemy, enemyAction);
    }

    public void EndTurn()
    {
        ally.OnTurnEnd();
        enemy.OnTurnEnd();
    }

    public void EndOpenTurn()
    {
        ally.OnTurnEnd();
        enemy.OnTurnEnd();
        combatResolver.EndPhase(ally, allyAction, enemy, enemyAction);
    }

    public void ProcessTurn()
    {
        SelectSkill();
        StartCombat();
    }

    public void ProcessOpenTurn(SkillData skill)
    {
        if (isProcessing) return;

        isProcessing = true;
        SelectSkill();
        allyAction = new SelectedAction(skill, ally);
        ally.SpendMana(allyAction.spentMana);
        enemy.SpendMana(enemyAction.spentMana);
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
                enemyAction = enemySkill,
                isAlly = true
            });

            comands.Add(
            new CombatCommand
            {
                type = CombatCommandType.ExecuteSkill,
                user = enemy,
                target = ally,
                allyAction = enemySkill,
                enemyAction = allySkill,
                isAlly = false
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
                enemyAction = allySkill,
                isAlly = false
            });

            comands.Add(
            new CombatCommand
            {
                type = CombatCommandType.ExecuteSkill,
                user = ally,
                target = enemy,
                allyAction = allySkill,
                enemyAction = enemySkill,
                isAlly = true
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
                        enemyAction = enemySkill,
                        isAlly = true
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
                        enemyAction = allySkill,
                        isAlly = false
                    });
            }
        }

        battlePresenter.StartCoroutine(battlePresenter.PlayCommands(comands));
    }

}
