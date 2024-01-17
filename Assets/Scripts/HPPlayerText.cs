using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPPlayerText : MonoBehaviour
{
    private TMP_Text HPText;

    private void Start()
    {
        HPText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        Player.OnHpChanged += UpdateHpText;
    }

    private void OnDisable()
    {
        Player.OnHpChanged -= UpdateHpText;
    }

    private void UpdateHpText(float hp)
    {
        HPText.text = "HP: " + hp.ToString();
    }
}
