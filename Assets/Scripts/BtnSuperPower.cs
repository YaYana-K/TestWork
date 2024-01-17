using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BtnSuperPower : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image cooldownImage;
    [SerializeField] TMP_Text cooldownText;

    private Button button;
    private Image image;

    private float cooldown = 2f;
    private float lastSuperAttackTime;
    private bool isCooldown = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        cooldownImage.fillAmount = 0;
        cooldownImage.gameObject.SetActive(false);
    }

    void Update()
    {
        
        
        if ( !player.CanAttackTarget() || Time.time - lastSuperAttackTime < cooldown)
        {
            Debug.Log(player.CanAttackTarget());
            button.interactable = false;
            image.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else if(!player.isDead)
        {
            Debug.Log(player.CanAttackTarget());
            button.interactable = true;
            image.color = new Color(1f, 1f, 1f, 1f);
        }
        if(isCooldown)
        {
            StartCooldown();
        }
    }

    public void OnButtonPress()
    {
        lastSuperAttackTime = Time.time;
        player.SuperAttack();
        cooldownImage.gameObject.SetActive(true);
        cooldownImage.fillAmount = 1;
        isCooldown = true;
    }

    private void StartCooldown()
    {
        cooldownImage.fillAmount -= 1 / cooldown * Time.deltaTime;
        cooldownText.text = cooldownImage.fillAmount.ToString("F1");
        if ( cooldownImage.fillAmount <= 0)
        {
            isCooldown = false;
            cooldownImage.gameObject.SetActive(false);
        }
    }
}
