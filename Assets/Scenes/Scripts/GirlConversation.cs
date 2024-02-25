using UnityEngine;
using DialogueEditor;

public class GirlConversation : MonoBehaviour
{
    public string dialogueObjectName; // Unique name or identifier for the dialogue object
    private NPCConversation myConversation;

    private void Start()
    {
        GameObject dialogueObject = GameObject.Find(dialogueObjectName);
        if (dialogueObject != null)
        {
            myConversation = dialogueObject.GetComponent<NPCConversation>();
            if (myConversation != null)
            {
                Debug.Log("Dialogue found: " + dialogueObjectName);
            }
            else
            {
                Debug.LogError("NPCConversation component not found in object: " + dialogueObjectName);
            }
        }
        else
        {
            Debug.LogError("Dialogue object not found: " + dialogueObjectName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (myConversation != null)
            {
                if (ConversationManager.Instance != null)
                {
                    // Start the conversation
                    ConversationManager.Instance.StartConversation(myConversation);
                }
                else
                {
                    Debug.LogError("ConversationManager.Instance is null");
                }
            }
            else
            {
                Debug.LogError("myConversation is null");
            }
        }
    }
}
