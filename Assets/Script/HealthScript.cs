using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Text healthText;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthText.text = "Health: " + currentHealth.ToString();

        if (currentHealth <= 0)
        {
            // Save the highest score to PlayerPrefs if the current time is higher
            if (PlayerPrefs.GetFloat("CurrentTime") > PlayerPrefs.GetFloat("HighScore"))
            {
                PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("CurrentTime"));
            }

            // Load the GameOver scene
            SceneManager.LoadScene("GameOver");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
