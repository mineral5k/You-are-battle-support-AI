using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlterPastBuffPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private AlterPastPanel alterPanel;
    [SerializeField] public selectedBuff locatedBuff;
    private AlterPastTurnButton turnButton;
    public bool isSelected = false;
    private Outline outline;

    public void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void Init(AlterPastPanel panel)
    {
        alterPanel = panel;
    }

    public void SetTurn(AlterPastTurnButton newTurnButton)
    {
        if (turnButton != null)
        {
            turnButton.locatedBuff = selectedBuff.None;
            turnButton.RefreshText();
        }

        turnButton = newTurnButton;
        turnButton.locatedBuff = locatedBuff;
        turnButton.RefreshText();

        isSelected = false;
        HideOutline();
    }

    public void ShowOutline()
    {
        outline.enabled = true;
    }

    public void HideOutline()
    {
        if (isSelected) return;
        outline.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isSelected == true)
        {
            isSelected = false;
            alterPanel.UnSelectBuff();
        }
        else if (isSelected == false)
        {
            isSelected = true;
            alterPanel.SelectBuff(this);
        }
        AudioManager.Instance.PlaySFX(AudioManager.Instance.AlterUIClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowOutline();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.AlterUIEnter);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideOutline();
    }
}
