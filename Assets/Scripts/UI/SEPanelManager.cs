using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SEPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject SEPanel;
    List<SEPanel> SEPanelList = new List<SEPanel>();
    public void Show(List<StatusEffectType> list)
    {
        int count = list.Count;
        int existCount = SEPanelList.Count;
        for (int i = 0; i < count - existCount; i++)
        {
            SEPanelList.Add(Instantiate(SEPanel, transform).GetComponent<SEPanel>());
        }

        for(int i = 0; i< count;i++)
        {
            SEPanelList[i].Show(list[i]);
        }
    }

    public void Hide()
    {
        foreach (SEPanel panel in SEPanelList)
        {
            panel.Hide();
        }
    }
}
