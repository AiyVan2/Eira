using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{


    // Default Damage
    private int defaultMechanicDamage = 10;
    private int defaultScholarDamage = 15;


    // Maximum Damage Cap
    private const int maxMechanicDamage = 35;  // Cap for mechanic damage
    private const int maxScholarDamage = 35;    // Cap for scholar damage


    // Start is called before the first frame update
    public int mechanicDamage;      // Default mechanic (melee) attack damage
    public int scholarDamage;        // Default scholar (ranged) attack damage
    public int maxHealth = 100;          // Default max health
    public int currentHealth;            // Current health
    public int upgradeCost = 50;         // Cost for upgrading damage

    void Start()
    {
        // Load saved stats
        mechanicDamage = PlayerPrefs.GetInt("MechanicDamage", mechanicDamage);
        scholarDamage = PlayerPrefs.GetInt("ScholarDamage", scholarDamage);
        maxHealth = PlayerPrefs.GetInt("MaxHealth", maxHealth);
        currentHealth = maxHealth; // Start at full health (you can adjust this logic)


        if(SceneManager.GetActiveScene().name == "Eira Beginning")
        {
            ResetDamage();
        }
    }

    // Save mechanic damage
    public void SaveMechanicDamage()
    {
        PlayerPrefs.SetInt("MechanicDamage", mechanicDamage);
        PlayerPrefs.Save();
    }

    // Save scholar damage
    public void SaveScholarDamage()
    {
        PlayerPrefs.SetInt("ScholarDamage", scholarDamage);
        PlayerPrefs.Save();
    }

    // Save max health
    public void SaveHealth()
    {
        PlayerPrefs.SetInt("MaxHealth", maxHealth);
        PlayerPrefs.Save();
    }



    // Resetting Player Damage
    public void ResetDamage()
    {
        mechanicDamage = defaultMechanicDamage;
        scholarDamage = defaultScholarDamage;
        SaveMechanicDamage();
        SaveScholarDamage();
    }
    // Method to upgrade Mechanic (melee) damage
    public bool UpgradeMechanicDamage(int upgradeAmount)
    {
        if (FindObjectOfType<PlayerLuminShards>().SpendCurrency(upgradeCost))
        {
            if (mechanicDamage + upgradeAmount <= maxMechanicDamage)
            {
                mechanicDamage += upgradeAmount;
                SaveMechanicDamage();  // Save the new damage
                return true;
            }
        }
        return false;
    }

    // Method to upgrade Scholar (ranged) damage
    public bool UpgradeScholarDamage(int upgradeAmount)
    {
        if (FindObjectOfType<PlayerLuminShards>().SpendCurrency(upgradeCost))
        {
            if (scholarDamage + upgradeAmount <= maxScholarDamage)
            {
                scholarDamage += upgradeAmount;
                SaveScholarDamage();  // Save the new damage
                return true;
            }
        }
        return false;
    }

    // Method to upgrade Health
    public bool UpgradeHealth(int upgradeAmount)
    {
        if (FindObjectOfType<PlayerLuminShards>().SpendCurrency(upgradeCost))
        {
            maxHealth += upgradeAmount;
            SaveHealth();  // Save the new health
            currentHealth = maxHealth;  // Restore full health when upgraded
            return true;
        }
        return false;
    }
}