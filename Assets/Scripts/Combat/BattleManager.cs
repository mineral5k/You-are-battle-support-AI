using System.Security;
using UnityEngine;

public class BattleManager
{
    UnitState ally;
    UnitState enemy;
    TurnProcesser turnProcesser;
    public int allyStartHP;

    public BattleManager(UnitState ally,UnitState enemy)
    {
        this.ally = ally;
        this.enemy = enemy;
        turnProcesser = new TurnProcesser(ally, enemy);
        allyStartHP = ally.currentHp;
    }


}
