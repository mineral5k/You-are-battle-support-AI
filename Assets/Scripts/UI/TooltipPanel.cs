using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TooltipPanel : MonoBehaviour
{
    [SerializeField] private SEPanelManager panelManager;
    [Space]
    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private TMP_Text cooldownText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private bool IsAllyUI = true;

    public void Show(SkillData skill, Transform target)
    {
        skillNameText.text = skill.skillName;

        descriptionText.text = skill.skillDescription;

        cooldownText.text = $"쿨타임 : {skill.cooltime}턴";

        manaText.text = $"{skill.fixedManaCost}마나";

        Vector3 offset = IsAllyUI ? new Vector3(0f, -1f, 0f) : new Vector3(-1.50f, 1.00f, 0f);

        transform.position = target.position + offset;

        gameObject.SetActive(true);

        panelManager.Show(skill.SEList);
        Canvas.ForceUpdateCanvases();

        LayoutRebuilder.ForceRebuildLayoutImmediate( (RectTransform)transform );
    }

    public void Show(StatusEffect effect, Transform target)
    {
        skillNameText.text = effect.EffectName;
        descriptionText.text = effect.EffectDescription;
        cooldownText.text = effect.Amount.ToString() + " Stack";
        manaText.text = "";
        transform.position = target.position + new Vector3(0f, -1f, 0f);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        if (panelManager != null)
        {
            panelManager.Hide();
        }
    }
}
