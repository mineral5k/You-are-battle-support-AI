using UnityEngine;

[System.Serializable]
public class SkillData 
{
    public string skillName;
    public ActionCategory category;
    public ManaCostType manaCostType;

    public int fixedManaCost;
    public int maxManaCost;

    public int cooltime;
    public int currentCooldown = 0;

    public int baseValue;
    public int valuePerMana;

    public int CalculateManaCost(int currentMana)
    {
        return manaCostType switch
        {
            ManaCostType.Fixed => fixedManaCost,
            ManaCostType.Variable => Mathf.Min(
                currentMana,
                maxManaCost),

            _ => 0
        };
    }

    public bool CanUse(int currentMana)
    {
        return (fixedManaCost >= currentMana) && (currentCooldown == 0);
    }

    public int CalculateValue(int spentMana)
    {
        return manaCostType switch
        {
            ManaCostType.Fixed => baseValue,
            ManaCostType.Variable => baseValue + valuePerMana * (spentMana - fixedManaCost),
            _ => 0
        };
    }

    public void Effect(UnitState ally, UnitState enemy)
    {

    }

    public void StartCooldown()
    {
        currentCooldown = cooltime;
    }

    public void TickCooldown()
    {
        if (currentCooldown == 0) return;
        else currentCooldown--;
    }
}
