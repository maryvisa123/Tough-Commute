using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for scene management

public class ToggleImages : MonoBehaviour
{
    public GameObject playImage;
    public GameObject pauseImage;
    public GameObject backImage; // Add reference to back image
    public CharacterController.CharacterController characterController;
    public Button toggleButton;

    private bool isPaused;

    private void Awake()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Attempt to find the CharacterController if it's not manually assigned
        if (characterController == null)
        {
            characterController = FindObjectOfType<CharacterController.CharacterController>();
        }
        
        // Check if the CharacterController script is successfully assigned
        if (characterController != null)
        {
            isPaused = characterController.IsCharacterStopped();
        }
        else
        {
            Debug.LogWarning("CharacterController script is not assigned!");
            isPaused = true; // Default to true if characterController is not assigned
        }

        // Set up the ToggleButton
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(HandleButtonClick);
        }
        else
        {
            Debug.LogWarning("ToggleButton is not assigned!");
        }

        // Update the UI to reflect the current state
        UpdateUI();
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Attempt to find the CharacterController after scene is loaded
        characterController = FindObjectOfType<CharacterController.CharacterController>();
        UpdateUI(); // Update UI to reflect the new character controller state
    }

    public void UpdateUI()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (characterController != null)
        {
            isPaused = characterController.IsCharacterStopped();
        }

        // Determine which image to show based on the scene
        if (currentScene != GameManager.Instance.mainSceneName && currentScene != GameManager.Instance.gameOverSceneName)
        {
            // In a non-main, non-game over scene, show the back image
            playImage.SetActive(false);
            pauseImage.SetActive(false);
            backImage.SetActive(true);
        }
        else
        {
            // In the main or game over scene, show play/pause images
            playImage.SetActive(!isPaused);
            pauseImage.SetActive(isPaused);
            backImage.SetActive(false);
        }
    }

    public void HandleButtonClick()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene != GameManager.Instance.mainSceneName && currentScene != GameManager.Instance.gameOverSceneName)
        {
            GameManager.Instance.GoToScene(GameManager.Instance.mainSceneName);
        }
        else
        {
            TogglePlayPause();
        }
    }

    public void TogglePlayPause()
    {
        if (characterController != null)
        {
            if (isPaused)
            {
                characterController.AllowMovement();
            }
            else
            {
                characterController.StopCharacter();
            }
            UpdateUI(); // Update the UI after changing the state
        }
        else
        {
            Debug.LogWarning("CharacterController script is not assigned!");
        }
    }
}
