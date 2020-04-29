using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("health")]
    [SerializeField] private Slider health;
    [SerializeField] private Transform healthPerk;
    [SerializeField] private GameObject healthIcon;
    [SerializeField] private Text healthText;
    [SerializeField] private Button healthPerkButton;

    [Header("dmg perk")]
    [SerializeField] private Transform dmgPerk;
    [SerializeField] private GameObject dmgIcon;
    [SerializeField] private Button dmgPerkButton;

    [Header("exp ui")]

    [Header("misc")]
    [SerializeField] private Text perkPoint;
    [SerializeField] private Text potionDisplay;

    [Header("unity things")]
    [SerializeField] private Transform iconContainer;
    [SerializeField] private PlayerStats player;

    private void Update()
    {
        PrintUI();
        CheckPerkPoints();
    }

    private void PrintUI()
    {
        PrintHealth();
        PrintPotions();
        PrintPerkPoints();
    }
    #region ingame UI
    private void PrintHealth()
    {
        health.value = player.currentHealth / player.maxHealth;
        healthText.text = player.currentHealth + "/" + player.maxHealth;
    }
    private void PrintPotions()
    {
        potionDisplay.text = "x " + player.healthPotions + " (F)";
    }
    private void PrintPerkPoints()
    {
        perkPoint.text = "Available Perk points " + player.perkPoint;
    }
    #endregion

    #region in inventory/perk
    private void CheckPerkPoints()
    {
        if (player.perkPoint == 0)
        {
            dmgPerkButton.interactable = false;
            healthPerkButton.interactable = false;
        }
        else
        {
            dmgPerkButton.interactable = true;
            healthPerkButton.interactable = true;
        }
    }
    public void IncreaseHealth()
    {
        if (player.perkPoint != 0)
        {
            player.healthPerk++;
            player.perkPoint--;
            player.HealthIncrease();
            AddHearth();
        }
    }
    private void AddHearth()
    {
        if (healthPerk == null)
            healthPerk = healthPerkButton.transform;

        healthPerk = Instantiate(healthIcon, healthPerk.position + Vector3.right * 100, Quaternion.identity, iconContainer).transform;
    }
    public void IncreaseDmg()
    {
        if (player.perkPoint != 0)
        {
            player.dmgPerk++;
            player.perkPoint--;
            player.DmgIncrease();
            AddDmg();
        }
    }
    private void AddDmg()
    {
        if (dmgPerk == null)
            dmgPerk = dmgPerkButton.transform;

        dmgPerk = Instantiate(dmgIcon, dmgPerk.position + Vector3.right * 100, dmgIcon.transform.rotation, iconContainer).transform;
    }
    public void ResetPerks()
    {
        player.ResetPerks();
        player.DmgIncrease();
        player.HealthIncrease();
    }
    #endregion
}
