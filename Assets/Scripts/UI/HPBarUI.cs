using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] private bool IsAlly = true;

    [Header("HP")]
    [SerializeField] private Image hpFill;
    [SerializeField] private TMP_Text hpText;

    [Header("Mana")]
    [SerializeField] private Image[] manaGems;
    [SerializeField] private Color activeManaColor = Color.white;
    [SerializeField] private Color inactiveManaColor = new Color(0.3f, 0.4f, 0.5f, 1f);
    [SerializeField] private Color shineManaColor = Color.red;

    [Header("Shield")]
    [SerializeField] private Image shieldIcon;
    [SerializeField] private TMP_Text shieldText;
    [SerializeField] private Color hpColor = Color.red;
    [SerializeField] private Color shieldColor = Color.skyBlue;

    [SerializeField] private SkillContainer skillContainer;
    [SerializeField] private SEIconBar iconBar;
    private UnitState unit;

    public void Bind(UnitState target)
    {
        unit = target;
        unit.OnStatusChanged += Refresh;
        skillContainer.Init(unit,this);

        Refresh();
    }

    public void Refresh()
    {
        if (unit == null)
            return;
        hpFill.color = hpColor;
        shieldIcon.gameObject.SetActive(false);

        float hpRatio = unit.maxHp <= 0 ? 0f : (float)unit.CurrentHp / unit.maxHp;

        hpFill.fillAmount = Mathf.Clamp01(hpRatio);

        hpText.text =
            $"{unit.CurrentHp} / {unit.maxHp}";

        int currentMana = unit.CurrentMana;
        for (int i = 0; i < manaGems.Length; i++)
        {
            if (manaGems[i] == null)
                continue;

            manaGems[i].color = i < currentMana ? activeManaColor : inactiveManaColor;
        }

        if (unit.shield > 0)
        {
            //hpFill.color = shieldColor;
            shieldIcon.gameObject.SetActive(true);
            shieldText.text = unit.shield.ToString();
        }

        skillContainer.Refresh();
        iconBar.Refresh(unit.StatusEffects);
    }

    public void ShineMana(SkillData skill)
    {
        if( !skill.CanUse(unit.CurrentMana) || !IsAlly )
        {
            return;
        }
        int RequiredMana = skill.CalculateManaCost(unit.CurrentMana);
        int currentMana = unit.CurrentMana;

        for (int i =0; i<currentMana; i++)
        {
            if (currentMana - RequiredMana <= i)
            {
                manaGems[i].color = shineManaColor;
            }
        }
        
    }
}
