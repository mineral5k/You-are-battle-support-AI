using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class UnitState 
{
    public int currentHp;
    public int currentMana;
    public int shield;
    public List<SkillData> skills = new List<SkillData>();
    public List<SkillData> Selectableskills = new List<SkillData>();
    private List<StatusEffect> statusEffects = new List<StatusEffect>();
    public List <StatusEffect> StatusEffects => statusEffects;


    public int TakeDamage(int damage)
    {
        damage = Mathf.Max(0, damage);

        int absorbedDamage = Mathf.Min(shield, damage);
        shield -= absorbedDamage;

        int hpDamage = damage - absorbedDamage;
        currentHp = Mathf.Max(0, currentHp - hpDamage);

        return hpDamage;
    }

    public void AddMana(int amount)
    {
        currentMana += Mathf.Max(0, amount);
    }

    public void AddShield(int amount)
    {
        shield += Mathf.Max(0, amount);
    }
    public List<SkillData> GetSelectableSkills()
    {
        return skills
            .Where(skill => skill.CanUse(currentMana))
            .ToList();
    }

    public void AddStatusEffect(StatusEffect newEffect)
    {
        StatusEffect existingEffect =statusEffects.Find(effect => effect.Id == newEffect.Id);

        if (existingEffect != null)
        {
            existingEffect.Merge(newEffect);

            return;
        }

        
        statusEffects.Add(newEffect);
    }

}
