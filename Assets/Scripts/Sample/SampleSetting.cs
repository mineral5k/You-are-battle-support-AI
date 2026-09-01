using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private RectTransform resultImage;
    [SerializeField] private Sprite victory;
    [SerializeField] private Sprite defeat;
    [SerializeField] private Sprite draw;
    private Vector2 targetPosition;
    void Start()
    {
        ally = new UnitState(50,2);
        enemy = new UnitState(80,3);
        ally.OnThisUnitDeath += AllyDie;
        enemy.OnThisUnitDeath += EnemyDie;
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
        targetPosition = resultImage.anchoredPosition;
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

    public void AllyDie()
    {
        if (bm.turnProcesser.isAltered == false) return;
        bm.turnProcesser.isBattleEnded = true;
        ally.anim.SetBool("Death", true);
        ShowResult(defeat,"Defeat");
    }

    public void EnemyDie()
    {
        if (bm.turnProcesser.isAltered == false) return;
        if (bm.turnProcesser.isBattleEnded == true)
        {
            enemy.anim.SetBool("Death", true);
            ShowResult(draw, "Draw");
        }
        else
        {
            bm.turnProcesser.isBattleEnded = true;
            enemy.anim.SetBool("Death", true);
            ShowResult(victory, "Victory");
        }
    }

    public void ShowResult(Sprite result, string rst)
    {
        Image image = resultImage.gameObject.GetComponent<Image>();
        image.sprite = result;

        AudioClip clip;

        switch (rst)
        {
            case "Victory":
                clip = AudioManager.Instance.Victory;
                break;
            case "Defeat":
                clip = AudioManager.Instance.Defeat;
                break;
            case "Draw":
                clip = AudioManager.Instance.Defeat;
                break;
            default:
                clip = AudioManager.Instance.Defeat; 
                break;
        }

        

        resultImage.DOKill();

        DG.Tweening.Sequence sequence = DOTween.Sequence();

        resultImage.anchoredPosition =
            targetPosition + Vector2.up * 500f;

        resultImage.gameObject.SetActive(true);
        sequence.Append
            (

             resultImage
                .DOAnchorPosY(targetPosition.y, 1.2f)
                .SetDelay(0.9f)
                .SetEase(Ease.OutBounce)
                .SetUpdate(true)

            );
        sequence.AppendCallback(() => AudioManager.Instance.PlaySFX(clip));

        

    }


}
