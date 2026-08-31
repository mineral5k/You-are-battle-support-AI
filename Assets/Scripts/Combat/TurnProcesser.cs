using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum selectedBuff
{
    None,
    AttackUp,
    GainShield
}

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
    public List<CombatCommand> commands = new List<CombatCommand>();
    public bool isProcessing = false;
    public bool isAltered = false;
    public bool isBattleEnded = false;
    public Action turnPanelRefresh;

    public int turn = 0;
    public List<selectedBuff> selectedBuffs = new List<selectedBuff> { selectedBuff.AttackUp, selectedBuff.GainShield, selectedBuff.None, selectedBuff.None, selectedBuff.None, };

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
        Debug.Log(allySkill.skillId);
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
    }

    public void StartCombat()
    {
        combatResolver.ResolveTurn(ally, allyAction, enemy, enemyAction);
    }

    public void EndTurn()
    {
        
    }

    public void EndOpenTurn(SelectedAction allyAction, SelectedAction enemyAction)
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
        if (isAltered == false) return;

        isProcessing = true;
        SelectSkill();
        allyAction = new SelectedAction(skill, ally);
        
        CreatComands(allyAction, enemyAction);
        battlePresenter.StartCoroutine(battlePresenter.PlayCommands(commands));
    }

    public void GetSelectedBuff()
    {
        switch(selectedBuffs[turn-1])
        {
            case selectedBuff.AttackUp:
                ally.AddStatusEffect(new AttackUp(4, 1));
                break;

            case selectedBuff.GainShield:
                ally.AddShield(10);
                break;

            default:
                break;
        }
    }

    public List<CombatCommand> CreatComands(SelectedAction allySkill, SelectedAction enemySkill)
    {
        ally.SpendMana(allySkill.spentMana);
        enemy.SpendMana(enemySkill.spentMana);
        commands.Clear();
        commands.Add(
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
            commands.Add(
            new CombatCommand
            {
                type = CombatCommandType.ExecuteSkill,
                user = ally,
                target = enemy,
                allyAction = allySkill,
                enemyAction = enemySkill,
                isAlly = true
            });

            commands.Add(
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
            commands.Add(
            new CombatCommand
            {
                type = CombatCommandType.ExecuteSkill,
                user = enemy,
                target = ally,
                allyAction = enemySkill,
                enemyAction = allySkill,
                isAlly = false
            });

            commands.Add(
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
            commands.Add(
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
                commands.Add(
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
                commands.Add(
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

        return commands;
    }

}
