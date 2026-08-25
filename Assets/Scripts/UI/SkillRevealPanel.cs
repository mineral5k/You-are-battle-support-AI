using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillRevealPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text skillName;
    [SerializeField] private TMP_Text powerText;

    public void SetSkill (SelectedAction action)
    {
        icon.sprite = action.skill.icon;
        skillName.text = action.skill.skillName;
        string text = action.skill.category == ActionCategory.Charge ? "" : $"À§·Â:{action.finalValue}";
        powerText.text = text;
    }
}
