using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{

    [SerializeField] private Image skillIcon;
    [SerializeField] private GameObject cooldownOverlay;
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private SampleSetting sampleSetting;
    private SkillData skill;
    private TooltipPanel tooltip;

    private HPBarUI hpBarUI;


    public void Init(SkillData skill,TooltipPanel tooltip,HPBarUI ui)
    {
        this.skill = skill;
        this.tooltip = tooltip;
        this.hpBarUI = ui;
        skillIcon.sprite = skill.icon;
    }

    public void Refresh()
    {
        if (skill == null)
            return;

        int cooldown = skill.currentCooldown;

        bool isCooldown = cooldown > 0;

        cooldownOverlay.SetActive(isCooldown);
        cooldownText.gameObject.SetActive(isCooldown);

        if (isCooldown)
        {
            cooldownText.text = cooldown.ToString();
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Show(skill, transform);
        hpBarUI.ShineMana(skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
        hpBarUI.Refresh();
    }

    public void OnClick()
    {
        sampleSetting.bm.turnProcesser.ProcessOpenTurn(skill);
    }
}
