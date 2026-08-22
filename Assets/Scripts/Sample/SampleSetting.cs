using UnityEngine;

public class SampleSetting : MonoBehaviour
{
    public UnitState ally;
    public UnitState enemy;
    public BattleManager bm;
    public HPBarUI allyUi;
    public HPBarUI enemyUi;
    void Start()
    {
        ally = new UnitState();
        enemy = new UnitState();
        allyUi.Bind(ally);
        enemyUi.Bind(enemy);
        bm = new BattleManager(ally, enemy);
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();

    }


}
