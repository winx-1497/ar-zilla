using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int maxHealth = 100; // Set the maximum health of the object
    private int currentHealth;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Set initial health to maximum
    }

    // Function to apply damage to the object
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthText.text = "Health: " + currentHealth.ToString();
        // Print a message to the console
        // Debug.Log("Health reduced! Current Health: " + currentHealth);

        // Check if the object should be destroyed

        if (currentHealth <= 0)
        {
            // Load a new scene (change "YourSceneName" to the actual name of your scene)
            SceneManager.LoadScene("GameOver");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

}
