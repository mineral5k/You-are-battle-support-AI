using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TooltipPanel : MonoBehaviour
{
    [SerializeField] private SEPanelManager panelManager;
    [Space]
    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private TMP_Text cooldownText;
    [SerializeField] private TMP_Text descriptionText;

    public void Show(SkillData skill, Transform target)
    {
        skillNameText.text =
            skill.skillName;

        descriptionText.text =
            skill.skillDescription;

        cooldownText.text =
            $"쿨타임 : {skill.cooltime}턴";

        manaText.text = $"{skill.fixedManaCost}마나";

        transform.position = target.position + new Vector3(0f, - 100f, 0f);

        gameObject.SetActive(true);

        panelManager.Show(skill.SEList);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        panelManager.Hide();
    }
}
