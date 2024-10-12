using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeMenu : MonoBehaviour
{
    private PlayerStats playerStats;
    public Text luminShardText;
    public Text mechanicDamageText;
    public Text scholarDamageText;
    private PlayerLuminShards playerLumin;

    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        playerLumin = player.GetComponent<PlayerLuminShards>();
    }

    void Update()
    {
        // Update UI with current values
        luminShardText.text = "Lumin Shards: " + playerLumin.currentCurrency;
        mechanicDamageText.text = "Mechanic Damage: " + playerStats.mechanicDamage + " (Max: 42)";
        scholarDamageText.text = "Scholar Damage: " + playerStats.scholarDamage + " (Max: 45)";
    }

    // Button function to upgrade mechanic damage
    public void UpgradeMechanic()
    {
        int upgradeAmount = 8;
        if (playerStats.UpgradeMechanicDamage(upgradeAmount))
        {
            Debug.Log("Mechanic damage upgraded!");
        }
        else
        {
            Debug.Log("Not enough Lumin Shards!");
        }
    }

    // Button function to upgrade scholar damage
    public void UpgradeScholar()
    {
        int upgradeAmount = 10;
        if (playerStats.UpgradeScholarDamage(upgradeAmount))
        {
            Debug.Log("Scholar damage upgraded!");
        }
        else
        {
            Debug.Log("Not enough Lumin Shards!");
        }
    }
}
