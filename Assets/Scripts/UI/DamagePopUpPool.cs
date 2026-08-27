using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpPool : MonoBehaviour
{
    public GameObject PopUpPrefab;
    public int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // 固府 按眉 积己
        for (int i = 0; i < poolSize; i++)
        {
            GameObject popUp = Instantiate(PopUpPrefab,transform);
            popUp.SetActive(false);

            pool.Enqueue(popUp);
        }
    }

    public DamagePopUp GetPopUp()
    {
        GameObject popUp = pool.Dequeue();

        popUp.SetActive(true);
        pool.Enqueue(popUp);

        return popUp.GetComponent<DamagePopUp>();
    }
}
