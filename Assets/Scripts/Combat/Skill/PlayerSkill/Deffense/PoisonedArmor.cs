using UnityEngine;

public class PoisonedArmor : SkillData
{
    public PoisonedArmor()
    {
        skillId = "poisonedAmor";
        skillName = "독 묻은 방어구";
        skillDescription = "마나를 최대 5까지 소모하여, 소모한 마나 당 방어도를 3 얻는다. 이번 턴에 피해를 받지 않았다면, 적에게 <color=#FF7043>[독]</color>을 1 부여한다.";
                            
        category = ActionCategory.Defense;

        manaCostType = ManaCostType.Variable;

        fixedManaCost = 1;
        maxManaCost = 5;

        baseValue = 3;
        valuePerMana = 3;

        cooltime = 4;
        SEList.Add(StatusEffectType.Shield);
        SEList.Add(StatusEffectType.Poison);
    }

    public override void Effect(UnitState ally, UnitState enemy, int value)
    {
        ally.AddShield(value);
        ally.AddStatusEffect(new PoisonReady(amount: 1, duration: 1));
    }
}
