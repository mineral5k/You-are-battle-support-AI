using UnityEngine;

public class SampleSetting : MonoBehaviour
{
    [SerializeField] private BattlePresenter battlePresenter;
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
        bm = new BattleManager(ally, enemy, battlePresenter);
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();

    }


}
