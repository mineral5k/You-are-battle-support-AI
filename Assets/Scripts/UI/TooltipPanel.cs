using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TooltipPanel : MonoBehaviour
{

    [SerializeField] private TMP_Text skillNameText;
    [SerializeField] private TMP_Text cooldownText;
    [SerializeField] private TMP_Text descriptionText;

    public void Show(SkillData skill, Transform target)
    {
        skillNameText.text =
            skill.skillName;

        descriptionText.text =
            skill.skillDescription;

        cooldownText.text =
            $"ÄðÅ¸ÀÓ : {skill.cooltime}ÅÏ";

        transform.position = target.position + new Vector3(0f, - 100f, 0f);

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
