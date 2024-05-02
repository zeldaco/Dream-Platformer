using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private float yThreshold = -40f;  // Initialize default threshold value
    private GravityController gravityController;

    private AudioManager audioManager; // Reference to the AudioManager

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        gravityController = GetComponent<GravityController>();
        audioManager = FindObjectOfType<AudioManager>(); // Get the AudioManager component

        // Check for null components and log errors if not found
        if (anim == null)
            Debug.LogError("Animator component not found on " + gameObject.name);
        if (gravityController == null)
            Debug.LogError("GravityController component not found on " + gameObject.name);
    }

    private void Update()
    {
        CheckFalling();  // Continuously check if the player has fallen
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");  // Trigger hurt animation
        }
        else if (!dead)
        {
            Die();
        }
        audioManager.PlaySFX(audioManager.damage); // Play damage sound effect
    }

    private void CheckFalling()
    {
        // Update yThreshold based on gravity direction
        yThreshold = gravityController.IsGravityUp ? 40f : -40f;

        if ((gravityController.IsGravityUp && transform.position.y > yThreshold) ||
            (!gravityController.IsGravityUp && transform.position.y < yThreshold))
        {
            if (!dead)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        anim.SetTrigger("die");
        GetComponent<PlayerMovement>().enabled = false; // Disable player movement script
        dead = true;
        Invoke("GameOver", 2f);  // Delay the game over to show death animations
        audioManager.PlaySFX(audioManager.death); // Play death sound effect
    }

    private void GameOver()
    {
        if (gravityController != null)
        {
            gravityController.ResetGravity();  // Reset gravity before reloading the scene
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        audioManager.PlaySFX(audioManager.respawn); // Play respawn sound effect
    }

    public void AddHealth(float _value) // For adding health when grabbing a heart
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        audioManager.PlaySFX(audioManager.heartGain); // Play health sound effect
    }
}
