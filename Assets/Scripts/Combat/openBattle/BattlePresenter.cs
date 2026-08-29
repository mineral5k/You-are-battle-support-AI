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

    [SerializeField] private Animator allyAnim;
    [SerializeField] private Animator enemyAnim;

    private SelectedAction allyAction;
    private SelectedAction enemyAction;

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
        yield return new WaitForSeconds(0.4f);
        turnProcesser.EndOpenTurn(allyAction,enemyAction);
        yield return new WaitForSeconds(0.3f);
        turnProcesser.StartTurn();
        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator ReplayBlindTurns()
    {
        for (int i = 0; i < 5; i++)
        {
            turnProcesser.GetSelectedBuff();
            SelectedAction allyAction = new SelectedAction(turnProcesser.allySkillRecord[i],turnProcesser.ally);
            SelectedAction enemyAction = new SelectedAction(turnProcesser.enemySkillRecord[i], turnProcesser.enemy);
            List<CombatCommand> commands = turnProcesser.CreatComands(allyAction, enemyAction);
            yield return PlayCommands(commands);
        }
    }


    private IEnumerator ShowSkill(CombatCommand command)
    {
        allyPanel.SetSkill(command.allyAction);
        enemyPanel.SetSkill(command.enemyAction);
        allyAction = command.allyAction;
        enemyAction = command.enemyAction;
        command.user.anim = allyAnim;
        command.target.anim = enemyAnim;
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
            yield return PlayAttackAnimation(command.user);

            command.allyAction.skill.Effect(command.user, command.target, command.allyAction.finalValue);

            yield return PlayHitAnimation(command.target);
        }
        else
        {
            command.allyAction.skill.Effect(command.user, command.target, command.allyAction.finalValue);

            yield return PlayNonAttackEffect(command.user);
        }

    }


    private IEnumerator PlayAttackAnimation(UnitState unit)
    {
        unit.anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
    }


    private IEnumerator PlayHitAnimation(UnitState unit)
    {
        yield return new WaitForSeconds(1.2f);
    }


    private IEnumerator PlayNonAttackEffect(UnitState unit)
    {
        unit.anim.SetBool("Defense", true);
        yield return new WaitForSeconds(0.7f);
        unit.anim.SetBool("Defense", false);
    }
}
