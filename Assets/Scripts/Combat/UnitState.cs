using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class UnitState 
{
    public int currentHp = 50;
    public int currentMana = 2;
    public int shield = 0;
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
        Selectableskills.Clear();
        foreach(SkillData skill in skills)
        {
            if(skill.CanUse(currentMana))
            {
                Selectableskills.Add(skill);
            }
        }
        return Selectableskills;
    }

    public void AddSkillData(SkillData skill)
    {
        skills .Add(skill);
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
