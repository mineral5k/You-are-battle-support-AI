using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private Image hpFill;
    [SerializeField] private TMP_Text hpText;

    [Header("Mana")]
    [SerializeField] private Image[] manaGems;
    [SerializeField] private Color activeManaColor = Color.white;
    [SerializeField] private Color inactiveManaColor = new Color(0.3f, 0.4f, 0.5f, 1f);

    [SerializeField] private SkillContainer skillContainer;
    private UnitState unit;

    public void Bind(UnitState target)
    {
        unit = target;
        unit.OnStatusChanged += Refresh;
        skillContainer.Init(unit);

        Refresh();
    }

    public void Refresh()
    {
        Debug.Log(unit.CurrentMana);
        if (unit == null)
            return;

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
        skillContainer.Refresh();
    }
}
