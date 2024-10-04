using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerLuminShards : MonoBehaviour
{
    private int defaultcurrentCurrency = 0;

    public int currentCurrency = 0; // Track how much currency the player has
    public Text currencyText; // UI element to display currency

    private void Start()
    {
        // Optionally, load saved currency data if you're saving the currency
        currentCurrency = PlayerPrefs.GetInt("PlayerLumin", 0);
        UpdateCurrencyUI();

        if(SceneManager.GetActiveScene().name == "Eira Beginning")
        {
            resetCurrency();
        }
    }

    // Method to add currency
    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
        UpdateCurrencyUI();


        // Optionally, save the new currency amount
        PlayerPrefs.SetInt("PlayerLumin", currentCurrency);
        PlayerPrefs.Save();
    }

    // Method to update the UI
    private void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = "Lumin Shards: " + currentCurrency.ToString();
        }
    }

    // Optional: You can create a method to spend currency if needed
    public bool SpendCurrency(int amount)
    {
        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            UpdateCurrencyUI();

            // Optionally, save the new currency amount
            PlayerPrefs.SetInt("PlayerLumin", currentCurrency);
            PlayerPrefs.Save();

            return true;
        }

        return false; // Not enough currency
    }

    public void resetCurrency()
    {
        currentCurrency = defaultcurrentCurrency;
        PlayerPrefs.SetInt("PlayerLumin", currentCurrency);
        PlayerPrefs.Save();
    }
}