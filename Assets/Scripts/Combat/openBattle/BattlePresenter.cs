using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BattlePresenter : MonoBehaviour
{
    private TurnProcesser turnProcesser;

    [SerializeField] private SkillRevealPanel allyPanel;
    [SerializeField] private RectTransform allyPanelTransform;
    [SerializeField] private SkillRevealPanel enemyPanel;
    [SerializeField] private RectTransform enemyPanelTransform;
    [SerializeField] private GameObject VS;

    private Vector2 allyPanelPos;
    private Vector2 enemyPanelPos;

    

    Vector3 normalScale = new Vector3(0.5f, 0.5f, 1f);

    public void Init(TurnProcesser turnProcesser)
    {
        this.turnProcesser = turnProcesser;
    }
    public IEnumerator PlayCommands(List<CombatCommand> commands)
    {
        allyPanelPos = allyPanelTransform.anchoredPosition;
        enemyPanelPos = enemyPanelTransform.anchoredPosition;

        foreach (CombatCommand command in commands)
        {
            switch (command.type)
            {
                case CombatCommandType.RevealSkill:

                    yield return ShowSkill(command);

                    break;


                case CombatCommandType.RevealClash:

                    yield return ShowClash(command);

                    break;


                case CombatCommandType.ExecuteSkill:

                    yield return ExecuteSkill(command);

                    break;
            }
        }

        allyPanel.HidePanel();
        enemyPanel.HidePanel();
        enemyPanelTransform.anchoredPosition = enemyPanelPos;
        allyPanelTransform.anchoredPosition = allyPanelPos;
        VS.SetActive(false);
        turnProcesser.EndOpenTurn();
        turnProcesser.StartTurn();
    }


    private IEnumerator ShowSkill(CombatCommand command)
    {
        allyPanel.SetSkill(command.allyAction);
        enemyPanel.SetSkill(command.enemyAction);
        VS.SetActive(true);

        allyPanel.transform.localScale = Vector3.one * 0.3f;
        allyPanel.gameObject.SetActive(true);
        allyPanel.transform.DOScale(normalScale, 0.2f).SetEase(Ease.OutBack);

        enemyPanel.transform.localScale = Vector3.one * 0.3f;
        enemyPanel.gameObject.SetActive(true);
        enemyPanel.transform.DOScale(normalScale, 0.2f).SetEase(Ease.OutBack);

        Debug.Log("스킬 보여주기");
        yield return new WaitForSeconds(0.7f);
    }
    



    private IEnumerator ShowClash(CombatCommand command)
    {
        bool isAllyWon = command.allyAction.finalValue > command.enemyAction.finalValue;
        bool isEnemyWon = command.enemyAction.finalValue > command.allyAction.finalValue;


        VS.SetActive(false);
        DG.Tweening.Sequence sequence = DOTween.Sequence();

        sequence.Append(allyPanelTransform.DOAnchorPosX(470f, 0.25f).SetEase(Ease.InCubic));
        sequence.Join(enemyPanelTransform.DOAnchorPosX(-450f, 0.25f).SetEase(Ease.InCubic));

        sequence.Append(allyPanelTransform.DOPunchScale(new Vector3(0.08f, 0.08f, 0f), 0.12f, 1, 0.3f));
        sequence.Join(enemyPanelTransform.DOPunchScale(new Vector3(0.08f, 0.08f, 0f), 0.12f, 1, 0.3f));

        sequence.AppendCallback(() =>
        {
            if (isAllyWon)
            {
                allyPanel.WinnerReaction(allyPanelPos,-1f);
                enemyPanel.BreakPanel(enemyPanelPos);
            }
            else if (isEnemyWon) 
            {
                enemyPanel.WinnerReaction(enemyPanelPos,1f);
                allyPanel.BreakPanel(allyPanelPos);
            }
            else
            {
                allyPanel.BreakPanel(allyPanelPos);
                enemyPanel.BreakPanel(enemyPanelPos);
                
            }
        });
        yield return new WaitForSeconds(1f);
    }


    private IEnumerator ExecuteSkill(CombatCommand command)
    {
        bool isAttack = command.allyAction.skill.category == ActionCategory.Attack;
        SkillRevealPanel panel = command.isAlly ? allyPanel : enemyPanel;
        panel.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0f), 0.2f, 1, 0.5f);

        if (isAttack)
        {
            // -------------------------
            // 공격 준비 애니메이션
            // -------------------------

            yield return PlayAttackAnimation(command.user);


            // -------------------------
            // 실제 공격 적용
            // -------------------------
            command.allyAction.skill.Effect(command.user, command.target, command.allyAction.finalValue);


            // -------------------------
            // 피격 연출
            // -------------------------

            yield return PlayHitAnimation(command.target);
        }
        else
        {
            // 방어 / 충전 등은
            // 여기서 실제 효과 적용
            command.allyAction.skill.Effect(command.user, command.target, command.allyAction.finalValue);

            yield return PlayNonAttackEffect(command.user);
        }

        Debug.Log("스킬 사용");
    }


    private IEnumerator PlayAttackAnimation(UnitState unit)
    {
        Debug.Log("공격 애니메이션");

        yield return new WaitForSeconds(0.5f);
    }


    private IEnumerator PlayHitAnimation(UnitState unit)
    {
        Debug.Log("피격 연출");

        yield return new WaitForSeconds(0.3f);
    }


    private IEnumerator PlayNonAttackEffect(UnitState unit)
    {
        Debug.Log("방어 / 버프 연출");

        yield return new WaitForSeconds(0.4f);
    }
}
