using UnityEngine;

public abstract class StatusEffect
{
    public abstract string Id { get; }
    public abstract string EffectName { get; }
    public abstract string EffectDescription { get; }
    public abstract bool IsBuff { get; }

    public int Amount { get; protected set; }
    public int RemainingTurns { get; protected set; }

    public bool IsExpired = false;

    protected StatusEffect(int amount, int duration)
    {
        Amount = amount;
        RemainingTurns = duration;
    }

    public virtual void OnTurnStart(UnitState owner)
    {

    }
    public virtual void OnTurnEnd(UnitState owner)
    {
    }

    public virtual void Merge(StatusEffect newEffect)
    {
        Amount += newEffect.Amount;

        //RemainingTurns = Mathf.Max(RemainingTurns, newEffect.RemainingTurns);
    }

    public void ProcessTurnEnd(UnitState owner)
    {
        OnTurnEnd(owner);
        //RemainingTurns--;
    }
}
