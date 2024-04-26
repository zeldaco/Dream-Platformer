using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public List<Image> hearts; // Reference to heart asset in UI
    private float defaultYThreshold = -10f; // Default threshold for falling into the void
    private float yThreshold; // Active threshold based on gravity
    private GravityController gravityController;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();
        gravityController = GetComponent<GravityController>(); // Assuming GravityController is on the same GameObject
        UpdateThreshold();
    }

    void Update()
    {
        UpdateThreshold(); // Ensure threshold is updated if gravity changes
        // Check if the player has fallen beyond the threshold
        if ((gravityController.IsGravityUp && transform.position.y > yThreshold) ||
            (!gravityController.IsGravityUp && transform.position.y < yThreshold))
        {
            GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with a monster
        if (collision.gameObject.CompareTag("Monster"))
        {
            LoseHeart();
        }
    }

    void LoseHeart()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHeartsUI();
        }
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateHeartsUI()
    {
        // Loop through all heart images
        for (int i = 0; i < hearts.Count; i++)
        {
            // Enable heart image if i < currentHealth, otherwise disable it
            hearts[i].enabled = i < currentHealth;
        }
    }

    void UpdateThreshold()
    {
        // Adjust threshold based on the direction of gravity
        yThreshold = gravityController.IsGravityUp ? defaultYThreshold : -defaultYThreshold;
    }

    void GameOver()
    {
        // Optionally add any game over logic or UI here
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
