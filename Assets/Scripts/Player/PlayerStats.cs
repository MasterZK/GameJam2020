using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("player attributes")]
    [SerializeField] public float currentHealth = 100;
    [SerializeField] public float maxHealth = 100;
    [SerializeField] private int baseHealth = 100;
    [SerializeField] private int dmg = 10;
    [SerializeField] private int baseDmg = 10;

    [Header("level system")]
    [SerializeField] public int level = 0;
    [SerializeField] public int exp = 0;
    [SerializeField] public int expRequirement = 20;
    [SerializeField] private int baseExpRequirement = 20; //for level 0 to 1

    [Header("perkpoint system")]
    [SerializeField] public int perkPoint = 0;
    [SerializeField] public int healthPerk = 0;
    [SerializeField] public int dmgPerk = 0;

    [Header("items")]
    [SerializeField] public int healthPotions = 0;

    [SerializeField] private Transform respawnPoint;
    public bool isDead = false;

    private void Update()
    {
        LevelUp();
        UsePotion();
    }

    private void ResetPlayer()
    {
        currentHealth = maxHealth;
        transform.position = respawnPoint.position;
        isDead = false;
    }
    private void PlayerDeath()
    {
        if (currentHealth > 0)
            return;

        isDead = true;
    }
    private void UsePotion()
    {
        if (healthPotions <= 0)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            currentHealth += 20;
            healthPotions--;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }

    #region Level System
    private void IncreaseExpRequired()
    {
        exp -= expRequirement;
        expRequirement = baseExpRequirement * level + 1;
    }
    private void LevelUp()
    {
        if (exp < expRequirement)
            return;

        level++;
        perkPoint++;
        currentHealth = maxHealth;
        IncreaseExpRequired();
    }
    #endregion

    #region Perk Point System
    public void HealthIncrease()
    {
        maxHealth = baseHealth + 20 * healthPerk;
        currentHealth = maxHealth;
    }
    public void DmgIncrease()
    {
        dmg = baseDmg + 5 * dmgPerk;
    }
    public void ResetPerks()
    {
        perkPoint = perkPoint + healthPerk + dmgPerk;
        healthPerk = 0;
        dmgPerk = 0;
    }
    #endregion

}
