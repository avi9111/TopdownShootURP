using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainForm : MonoBehaviour
{
    public Image healthBar;//血条
    //public TMP_Text healthText;//血量文字
    public Text healthText;
    [Header("暂时打开菜单用")]
    public Image characterIcon;
    private void Update() {
        OnOnplayerHealthChanged(PlayerStats.instance.startHealth, PlayerStats.instance.health);

        EventTriggerListener.Get(characterIcon.gameObject).onClick = OnCharacterClick;
    }

    void OnCharacterClick(GameObject sender)
    {
        PlayerStats.instance.showGameCMenu = true;
        ResourceManager.instance.gameOverMenu.SetActive(true);
    }

    private void OnOnplayerHealthChanged(float health, float startHealth)
    {
        healthBar.fillAmount = startHealth / health;
        //  healthText.SetText($"{health}/{startHealth}");
        healthText.text = $"{health}/{startHealth}";
    }
}
