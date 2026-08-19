using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SkillContainer : MonoBehaviour
{
    [SerializeField] private List<SkillButtonUI> skillButtons = new List<SkillButtonUI>();
    [SerializeField] private TooltipPanel tooltip;
    private UnitState unit;
    
    
    public void Init(UnitState unit)
    {
        this.unit = unit;
        for (int i = 0; i<skillButtons.Count;i++)
        {
            skillButtons[i].Init(unit.skills[i],tooltip);
        }
    }

    public void Refresh()
    {
        foreach (SkillButtonUI button in skillButtons)
        {
            button.Refresh();
        }
    }
}
