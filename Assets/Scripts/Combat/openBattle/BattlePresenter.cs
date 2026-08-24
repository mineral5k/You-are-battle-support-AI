using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePresenter : MonoBehaviour
{
    [SerializeField] private CombatResolver resolver;

    public IEnumerator PlayCommands(List<CombatCommand> commands)
    {
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
    }


    private IEnumerator ShowSkill(CombatCommand command)
    {
        // 여기서 나중에
        // SkillPopupUI.Show(...)
        // 등을 실행

        yield return new WaitForSeconds(0.7f);
    }


    private IEnumerator ShowClash(CombatCommand command)
    {
        // 나중에
        //
        // clashUI.Show(
        //     command.action,
        //     command.opponentAction);

        yield return new WaitForSeconds(1f);
    }


    private IEnumerator ExecuteSkill(CombatCommand command)
    {
        bool isAttack = command.allyAction.skill.category == ActionCategory.Attack;


        if (isAttack)
        {
            // -------------------------
            // 공격 준비 애니메이션
            // -------------------------

            yield return PlayAttackAnimation(command.user);


            // -------------------------
            // 실제 공격 적용
            // -------------------------

            // resolver.ExecuteCommand(command);


            // -------------------------
            // 피격 연출
            // -------------------------

            yield return PlayHitAnimation(command.target);
        }
        else
        {
            // 방어 / 충전 등은
            // 여기서 실제 효과 적용

            //resolver.ExecuteCommand(command);

            yield return PlayNonAttackEffect(command.user);
        }
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
