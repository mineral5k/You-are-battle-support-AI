using UnityEngine;

public class SampleSetting : MonoBehaviour
{
    public UnitState ally;
    public UnitState enemy;
    public BattleManager bm;
    void Start()
    {
        ally = new UnitState();
        enemy = new UnitState();
        ally.AddMana(3);
        ally.AddSkillData(new FlameStrike());
        enemy.AddSkillData(new FlameStrike());
        bm = new BattleManager(ally, enemy);
        bm.turnProcesser.ProcessTurn();
        
        
    }

    
}
