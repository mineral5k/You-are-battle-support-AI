using UnityEngine;


public enum CombatCommandType
{
    RevealSkill,
    RevealClash,
    ExecuteSkill
}
public class CombatCommand
{
    public CombatCommandType type;

    public UnitState user;
    public UnitState target;

    public SelectedAction allyAction;

    // 공격 VS 공격 표시용
    public SelectedAction enemyAction;
}
