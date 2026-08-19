using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{

    [SerializeField] private Image skillIcon;
    [SerializeField] private GameObject cooldownOverlay;
    [SerializeField] private TextMeshProUGUI cooldownText;

    private SkillData skill;
    private TooltipPanel tooltip;


    public void Init(SkillData skill,TooltipPanel tooltip)
    {
        this.skill = skill;
        this.tooltip = tooltip;
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
    }
}
