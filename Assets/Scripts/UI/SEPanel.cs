using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SEPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text description;

    public void Show(StatusEffectType type)
    {
        StatusEffectsSO SO = Resources.Load<StatusEffectsSO>($"ScriptableObjects/StatusEffects/{type}"); 
        icon.sprite = SO.icon;
        name.text = SO.name;
        description.text = SO.description;
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
