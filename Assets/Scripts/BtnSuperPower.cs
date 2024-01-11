using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BtnSuperPower : MonoBehaviour
{
    [SerializeField] private Player player;

    private Button button;
    private Image image;

    private float cooldown = 2f;
    private float lastSuperAttackTime;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        if(Time.time - lastSuperAttackTime < cooldown)
        {
            button.interactable = false;
            image.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            button.interactable = true;
            image.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void OnButtonPress()
    {
        lastSuperAttackTime = Time.time;
        player.SuperAttack();
    }
}
