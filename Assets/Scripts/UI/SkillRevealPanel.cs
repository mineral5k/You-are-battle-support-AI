using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SkillRevealPanel : MonoBehaviour
{
    [SerializeField] public Image icon;
    [SerializeField] private TMP_Text skillName;
    [SerializeField] private TMP_Text powerText;

    public void SetSkill (SelectedAction action)
    {
        icon.sprite = action.skill.icon;
        skillName.text = action.skill.skillName;
        string text = action.skill.category == ActionCategory.Charge ? "" : $"À§·Â:{action.finalValue}";
        powerText.text = text;
    }

    public void HidePanel()
    {
        gameObject.transform.DOScale(Vector3.one * 0.3f, 0.2f).SetEase(Ease.InBack).OnComplete(
            () =>
            {
                gameObject.SetActive(false);
                gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            });
    }

    public void BreakPanel(Vector2 originalPos)
    {
        DG.Tweening.Sequence breakSequence = DOTween.Sequence();
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        breakSequence.Append(rectTransform.DOAnchorPos(originalPos, 0.25f).SetEase(Ease.OutCubic));
        breakSequence.JoinCallback(() => AudioManager.Instance.PlaySFX(AudioManager.Instance.ClashBreak));
        breakSequence.Append(transform.DOPunchRotation(new Vector3(0f, 0f, 12f), 0.15f, 5, 0.5f));
        breakSequence.Append(transform.DOScale(new Vector3(0.15f, 0.65f, 1f), 0.12f).SetEase(Ease.InBack));

        breakSequence.OnComplete(() =>
        {
            transform.gameObject.SetActive(false);
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            transform.localRotation = Quaternion.identity;
        });
    }

    public void WinnerReaction(Vector2 originalPos, float direction)
    {
        RectTransform winner = gameObject.GetComponent<RectTransform>();
        DG.Tweening.Sequence sequence = DOTween.Sequence();

        sequence.Append(winner.DOAnchorPos(originalPos, 0.25f).SetEase(Ease.OutCubic));
    }
}
