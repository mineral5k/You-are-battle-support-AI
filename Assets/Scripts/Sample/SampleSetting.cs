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
    void Start()
    {
        ally = new UnitState();
        enemy = new UnitState();
        allyUi.Bind(ally);
        enemyUi.Bind(enemy);
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

    public void TurnTextRefresh()
    {
        turnText.text = $"Turn {bm.turnProcesser.turn}";
    }


}
