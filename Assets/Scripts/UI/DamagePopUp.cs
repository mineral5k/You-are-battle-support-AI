using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] private float launchForceX = 3f; // 오른쪽으로 미는 힘
    [SerializeField] private float launchForceY = 5f; // 위로 미는 힘
    [SerializeField] private TextMeshProUGUI damageText;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PopUp(string damage)
    {
        damageText.text = damage;
        gameObject.SetActive(true);
        Vector2 launchDirection = new Vector2(launchForceX, launchForceY);
        rb.AddForce(launchDirection, ForceMode2D.Impulse);
        StartCoroutine(PopUpCoroutine());
    }

    IEnumerator PopUpCoroutine()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
