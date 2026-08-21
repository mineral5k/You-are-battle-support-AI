using UnityEngine;

public enum StatusEffectType
{
    Burn,
    Poison,
    AttackUp,
    Shield
}

[CreateAssetMenu(fileName = "StatusEffectsSO", menuName = "Scriptable Objects/StatusEffectsSO")]
public class StatusEffectsSO : ScriptableObject
{
    public Sprite icon;
    public string name;
    public string description; 
}
