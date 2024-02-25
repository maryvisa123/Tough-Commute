using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConversationDND : MonoBehaviour
{
    void Awake()
    {
        // Find all EventSystems in the current scene
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();

        // If there's more than one EventSystem (the current one), destroy the other ones
        if (eventSystems.Length > 1)
        {
            foreach (var eventSystem in eventSystems)
            {
                // Check if the found EventSystem is not this GameObject's EventSystem
                if (eventSystem.gameObject != this.gameObject)
                {
                    Destroy(eventSystem.gameObject);
                }
            }
        }

        DontDestroyOnLoad(this.gameObject);
    }
}