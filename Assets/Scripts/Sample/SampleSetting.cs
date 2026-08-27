using TMPro;
using UnityEngine;

public class SampleSetting : MonoBehaviour
{
    [SerializeField] private BattlePresenter battlePresenter;
    public UnitState ally;
    public UnitState enemy;
    public BattleManager bm;
    public HPBarUI allyUi;
    public HPBarUI enemyUi;
    public TMP_Text turnText;
    public DamagePopUpPool damagePopUpPool;
    void Start()
    {
        ally = new UnitState();
        enemy = new UnitState();
        allyUi.Bind(ally);
        enemyUi.Bind(enemy);
        ally.pool = damagePopUpPool;
        enemy.pool = damagePopUpPool;
        bm = new BattleManager(ally, enemy, battlePresenter);
        bm.turnProcesser.turnPanelRefresh += TurnTextRefresh;
        bm.turnProcesser.StartTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
        bm.ProcessBlindTurn();
    }

    public void PlayOpenTurn(SkillData skill)
    {
        bm.turnProcesser.ProcessOpenTurn(skill);
    }

    public void ReplayBlindTurns()
    {
        ResetConditions();
        battlePresenter.StartCoroutine(battlePresenter.ReplayBlindTurns());
    }

    public void ResetConditions()
    {
        ally.ResetCondition();
        enemy.ResetCondition();
        bm.turnProcesser.turn = 0;
        bm.turnProcesser.StartTurn();
    }

    public void TurnTextRefresh()
    {
        turnText.text = $"Turn {bm.turnProcesser.turn}";
    }


}
