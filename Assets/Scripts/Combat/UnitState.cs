using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class UnitState
{
    public UnitState target;
    public event Action OnStatusChanged;
    public event Action OnThisUnitDeath;
    public int maxHp;
    private int currentHp;
    public int CurrentHp
    {
        get => currentHp; 
        private set
        {
            int newHp = Mathf.Clamp(value, 0, maxHp);
            currentHp = newHp;
            OnStatusChanged?.Invoke();
            if (currentHp ==0) OnThisUnitDeath?.Invoke();
        } 
    }
    private int MaxMana = 10;
    private int startMana;
    private int currentMana;
    public int CurrentMana
    {
        get => currentMana;

        private set
        {
            int newMana = Mathf.Clamp(value, 0, MaxMana);

            if (currentMana == newMana)
                return;

            currentMana = newMana;

            OnStatusChanged?.Invoke();
        }
    }
    public int shield = 0;
    public List<SkillData> skills = new List<SkillData>();
    public List<SkillData> Selectableskills = new List<SkillData>();
    private List<StatusEffect> statusEffects = new List<StatusEffect>();
    public List <StatusEffect> StatusEffects => statusEffects;
    public bool IsDamagedThisTurn = false;
    public int bookedDamage = 0;

    public Animator anim;
    public DamagePopUpPool pool;

    public int AttackUp
    {
        get
        {
            int amount = 0;
            StatusEffect AttackUpEffect = statusEffects.Find(effect => effect.Id =="AttackUp");
            if (AttackUpEffect != null) amount = AttackUpEffect.Amount;
            return amount;
        }
    }


    public UnitState(int maxHp ,int startMana)
    {
        this.maxHp = maxHp;
        currentHp = maxHp;
        this.startMana = startMana;
        currentMana = startMana;

        AddSkillData(new DefaultAttack());
        AddSkillData(new FlameStrike());
        AddSkillData(new DefaultDeffense());
        AddSkillData(new PoisonedArmor());
        AddSkillData(new DefaultCharge());
        AddSkillData(new PowerCharge());
        AddStatusEffect(new StartMana(amount: startMana, duration: 3));

    }

    public int TakeDamage(int damage)
    {
        damage = Mathf.Max(0, damage);

        int absorbedDamage = Mathf.Min(shield, damage);
        shield -= absorbedDamage;

        int hpDamage = damage - absorbedDamage;
        if (hpDamage > 0)
        {
            IsDamagedThisTurn = true;
            if (anim != null)
            {
                DamagePopUp damagePopUp = pool.GetPopUp();
                damagePopUp.transform.position = anim.transform.position + new Vector3 (1.5f,1f,0);
                damagePopUp.PopUp(hpDamage.ToString());
                anim.SetTrigger("Hurt");
            }
        }
        else
        {
            if (anim != null)
            {
                DamagePopUp damagePopUp = pool.GetPopUp();
                damagePopUp.transform.position = anim.transform.position + new Vector3(1.5f, 1f, 0);
                damagePopUp.PopUp("Blocked");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.BlockSound);
            }
        }
        CurrentHp = Mathf.Max(0, CurrentHp - hpDamage);
        OnStatusChanged?.Invoke();

        return hpDamage;
    }

    public void AddMana(int amount)
    {
        CurrentMana += Mathf.Max(0, amount);
    }

    public void SpendMana(int amount)
    {
        CurrentMana -= Mathf.Max(0, amount);
    }

    public void AddShield(int amount)
    {
        shield += Mathf.Max(0, amount);
        OnStatusChanged?.Invoke();
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

        OnStatusChanged?.Invoke();
    }

    public void OnTurnStart()
    {
        IsDamagedThisTurn = false;
        CurrentMana++;
        foreach (StatusEffect Effect in statusEffects.ToArray())
        {
            Effect.OnTurnStart(this);
        }
        OnStatusChanged?.Invoke();
    }

    public void OnTurnEnd()
    {
        foreach(StatusEffect Effect in statusEffects)
        {
            Effect.ProcessTurnEnd(this);
            if (Effect.Amount <=0)
            {
                Effect.IsExpired = true;
            }
        }

        statusEffects.RemoveAll(effect => effect.IsExpired);
        OnStatusChanged?.Invoke();
        if (bookedDamage > 0)
        {
            TakeDamage(bookedDamage);
        }
        bookedDamage = 0;
    }

    public void ResetCondition()
    {
        currentHp = maxHp;
        StatusEffects.Clear();
        currentMana = startMana;
        foreach(SkillData skill in skills)
        {
            skill.currentCooldown = 0;
        }
    }


}
