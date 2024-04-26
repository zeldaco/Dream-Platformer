using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private float yThreshold = -30f;  // Initialize default threshold value
    private GravityController gravityController;

    private void Awake() 
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        gravityController = GetComponent<GravityController>();  // Assuming the GravityController is attached to the same GameObject
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
    }

    private void CheckFalling()
    {
        // Update yThreshold based on gravity direction
        yThreshold = gravityController.IsGravityUp ? 30f : -30f; // Ensure yThreshold is set correctly

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
    }

    private void GameOver()
    {
        if (gravityController != null)
        {
            gravityController.ResetGravity();  // Reset gravity before reloading the scene
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

        public void AddHealth(float _value) //for adding health when grabbing a heart
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

}
