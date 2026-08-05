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
        bm = new BattleManager(ally, enemy);
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();

    }


}
