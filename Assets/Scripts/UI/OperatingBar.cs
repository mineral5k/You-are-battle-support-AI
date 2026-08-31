using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OperatingBar : MonoBehaviour
{
    [SerializeField] private Image loadingFill;

    [SerializeField] private float loadingDuration = 5f;

    private void Awake()
    {
        loadingFill.fillAmount = 0f;

        loadingFill
            .DOFillAmount(1f, loadingDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Hide();
            });
    }

    public void Hide()
    {
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(gameObject.transform.DOScale(Vector3.one * 0.1f, 0.13f));
        sequence.AppendCallback(() => gameObject.SetActive(false));
    }
}
