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

    public int turn = 0;

    public TurnProcesser(UnitState ally, UnitState enemy)
    {
        this.ally = ally;
        this.enemy = enemy;
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

}
