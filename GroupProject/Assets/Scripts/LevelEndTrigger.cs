using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    // Set this in the inspector to the name of the scene you want to load
    public string sceneToLoad = "LevelName"; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            ResetLevel();
        }
    }

    // Function to reset the level or load a new scene
    void ResetLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(sceneToLoad);
        // Or reset the game state if needed
    }
}
