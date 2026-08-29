using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlterPastTurnButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private AlterPastPanel alterPanel;
    [SerializeField] public TMP_Text buffText;
    [SerializeField] public selectedBuff locatedBuff;
    private bool isSelected = false;
    private Outline outline;

    public void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void Init(AlterPastPanel panel)
    {
        alterPanel = panel;
    }

    public void ShowOutline()
    {
        outline.enabled = true;
    }

    public void HideOutline()
    {
        outline.enabled = false;
    }

    public void RefreshText()
    {
        switch (locatedBuff)
        {
            case selectedBuff.None:
                buffText.text = "";
                break;

            case selectedBuff.AttackUp:
                buffText.text = "공격 강화";
                break;

            case selectedBuff.GainShield:
                buffText.text = "방어 강화";
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        alterPanel.SelectTurn(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowOutline();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideOutline();
    }
}
