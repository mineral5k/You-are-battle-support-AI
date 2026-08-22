using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SEIconBar : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    [SerializeField] private TooltipPanel tooltipPanel;
    private List<SEIcon> iconList = new List<SEIcon>();

    public void Refresh(List<StatusEffect> list)
    {
        int count = list.Count;
        int existCount = iconList.Count;
        for (int i = 0; i < count - existCount; i++)
        {
            iconList.Add(Instantiate(icon, transform).GetComponent<SEIcon>());
            iconList.Last().Init(tooltipPanel);
        }

        for (int i = 0;i < iconList.Count; i++)
        {
            if (i < list.Count)
            {
                iconList[i].Refresh(list[i]);
                iconList[i].gameObject.SetActive(true);
            }
            else
            {
                iconList[i].gameObject.SetActive(false);
            }
        }
    }
}
