using UnityEngine;

public class FlameStrike : SkillData
{
    private int burnAmount = 5;

    private int burnDuration=5;

    public FlameStrike()
    {
        skillId = "flameStrike";
        skillName = "화염 강타";
        skillDescription = "마나를 최대 6까지 소모하여, 소모한 마나당 3의 위력으로 적을 공격한다. 피해를 입혔다면 <color=#FF7043>[화상]</color>을 5부여한다.";
        category = ActionCategory.Attack;

        manaCostType = ManaCostType.Variable;

        fixedManaCost = 1;
        maxManaCost = 6;

        baseValue = 3;
        valuePerMana = 3;

        cooltime = 3;

        SEList.Add(StatusEffectType.Burn);
    }

    public override void Effect( UnitState ally, UnitState enemy, int value )
    {
        int hpDamage = enemy.TakeDamage(value);

        if (hpDamage <= 0)
            return;

        enemy.AddStatusEffect( new Burn(damage: burnAmount, duration: burnDuration));
    }
}
