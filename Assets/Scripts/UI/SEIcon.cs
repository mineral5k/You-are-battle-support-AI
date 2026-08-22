using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SEIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text amount;
    StatusEffect effect;

    [SerializeField] TooltipPanel tooltip;

    public void Init(TooltipPanel tooltip)
    {
        this.tooltip = tooltip; 
    }
    public void Refresh(StatusEffect effect)
    {
        this.effect = effect;
        icon.sprite = Resources.Load<Sprite>($"EffectIcon/{effect.Id}");
        amount.text = effect.Amount.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Show(effect, transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
    }

}
