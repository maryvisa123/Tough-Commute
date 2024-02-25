using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PersistCanvas : MonoBehaviour
{
    public static PersistCanvas Instance { get; private set; }
    public List<string> scenesWithoutUI; // Add names of scenes without UI here

    private Canvas canvas;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            canvas = GetComponent<Canvas>();

            // Subscribe to the scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Check for multiple EventSystems and remove extras
            EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();
            if (eventSystems.Length > 1)
            {
                Destroy(eventSystems[1].gameObject); // Destroy the second EventSystem if it exists
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the current scene is in the list of scenes without UI
        canvas.enabled = !scenesWithoutUI.Contains(scene.name);
    }
}
