using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Vector2 playerLastPosition; // Stores the player's last position
    public Vector2 positionOffset = new Vector2(1.0f, 1.0f); // Default offset, adjustable in Inspector
    public string mainSceneName = "Main Scene"; // Set the name of your main scene here
    public string gameOverSceneName = "GameOverScene"; // Set the name of your game over scene here

    private void Awake()
    {
        // Ensure that only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Method called to switch scenes
    public void GoToScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == mainSceneName)
        {
            // Save the player's position with offset before switching scenes
            CharacterController.CharacterController characterController = FindObjectOfType<CharacterController.CharacterController>();
            if (characterController != null)
            {
                playerLastPosition = new Vector2(characterController.transform.position.x, characterController.transform.position.y) + positionOffset;
                Debug.Log("Saving player position: " + playerLastPosition);
            }
        }

        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }

    // Call this method after a scene is loaded to restore the player's position
    public void RestorePlayerPosition(CharacterController.CharacterController characterController)
    {
        if (characterController != null && SceneManager.GetActiveScene().name == mainSceneName)
        {
            characterController.transform.position = playerLastPosition;
            Debug.Log("Restored player position: " + playerLastPosition);
        }
    }

    // Overloaded version of GoToScene for Unity Events (only requires scene name)
    public void GoToScene(string sceneName, Vector2 positionOffset)
    {
        GoToScene(sceneName); // Call the main GoToScene method
    }
}
