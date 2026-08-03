using UnityEngine;

public class FlameStrike : SkillData
{
    private const int BurnAmount = 5;

    private readonly int burnDuration=5;

    public FlameStrike()
    {
        skillName = "È­¿° °­Å¸";
        category = ActionCategory.Attack;

        manaCostType = ManaCostType.Variable;

        fixedManaCost = 0;
        maxManaCost = 6;

        baseValue = 0;
        valuePerMana = 3;
    }

    public override void Effect( UnitState ally, UnitState enemy, int value )
    {
        int hpDamage = enemy.TakeDamage(value);

        if (hpDamage <= 0)
            return;

        enemy.AddStatusEffect(
            new Burn(
                damage: BurnAmount,
                duration: burnDuration));
    }
}
