using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [SerializeField]
    private Image hpFill;

    [SerializeField]
    private TMP_Text hpText;

    private UnitState unit;

    public void Bind(UnitState target)
    {
        unit = target;
        unit.OnStatusChanged += Refresh;
        Refresh();
    }

    public void Refresh()
    {
        if (unit == null)
            return;

        float hpRatio = unit.maxHp <= 0 ? 0f : (float)unit.currentHp / unit.maxHp;

        hpFill.fillAmount = Mathf.Clamp01(hpRatio);

        hpText.text =
            $"{unit.currentHp} / {unit.maxHp}";
    }
}
