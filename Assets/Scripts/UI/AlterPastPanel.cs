using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AlterPastPanel : MonoBehaviour
{
    [SerializeField] List<AlterPastTurnButton> turnButtons = new List<AlterPastTurnButton>();
    [SerializeField] List<AlterPastBuffPanel> buffPanels = new List<AlterPastBuffPanel>();
    [SerializeField] private SampleSetting sampleSetting;


    private selectedBuff buff = selectedBuff.None;
    AlterPastBuffPanel selectedBuffPanel;

    public void Awake()
    {
        foreach (AlterPastTurnButton button in turnButtons)
        {
            button.Init(this);
        }

        foreach (AlterPastBuffPanel button in buffPanels)
        {
            button.Init(this);
        }
    }
    public void SelectBuff(AlterPastBuffPanel panel)
    {
        if (selectedBuffPanel != null)
        {
            selectedBuffPanel.isSelected = false;
            selectedBuffPanel.HideOutline();
        }
        selectedBuffPanel = panel;
        buff = panel.locatedBuff;
    }

    public void UnSelectBuff()
    {
        selectedBuffPanel = null;
        buff = selectedBuff.None;
    }

    public void SelectTurn(AlterPastTurnButton button)
    {
        if (selectedBuffPanel == null) return;

        selectedBuffPanel.SetTurn(button);
        buff = selectedBuff.None;
        selectedBuffPanel = null;

    }

    public void ShowPanel()
    {
        gameObject.transform.localScale = Vector3.one * 0.5f;
        gameObject.SetActive(true);
        gameObject.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
    }

    public void HidePanel()
    {
        gameObject.transform.DOScale(Vector3.one * 0.5f, 0.2f).SetEase(Ease.OutBack);
        gameObject.SetActive(false);
    }

    public void AlterThePast()
    {
        HidePanel();
        for (int i = 0; i < 5; i++)
        {
            sampleSetting.bm.turnProcesser.selectedBuffs[i] = turnButtons[i].locatedBuff;
        }
        sampleSetting.ReplayBlindTurns();
    }
}
