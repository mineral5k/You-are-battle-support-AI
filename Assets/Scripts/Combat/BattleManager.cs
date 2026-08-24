using NUnit.Framework;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class BattleManager
{
    public UnitState ally;
    public UnitState enemy;
    public TurnProcesser turnProcesser;
    public int allyStartHP;
    public List<SkillData> allySkillRecord = new List<SkillData>();
    public List<SkillData> enemySkillRecord = new List<SkillData>();

    public BattleManager(UnitState ally,UnitState enemy, BattlePresenter battlePresenter)
    {
        this.ally = ally;
        this.enemy = enemy;
        ally.target = enemy;
        enemy.target = ally;
        turnProcesser = new TurnProcesser(ally, enemy, battlePresenter);
        allyStartHP = ally.CurrentHp;
    }

    public void ProcessBlindTurn()
    {
        turnProcesser.ProcessTurn();
        allySkillRecord.Add(turnProcesser.allyAction.skill);
        Debug.Log(turnProcesser.allyAction.skill.skillName);
        enemySkillRecord.Add(turnProcesser.enemyAction.skill);
        Debug.Log(turnProcesser.enemyAction.skill.skillName);
        turnProcesser.EndTurn();
        turnProcesser.StartTurn();
    }

    public void ProcessOpenTurn()
    {

    }

}
