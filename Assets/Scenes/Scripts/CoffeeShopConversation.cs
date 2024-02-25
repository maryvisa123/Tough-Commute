using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class CoffeeShopConversation : MonoBehaviour
{
    
    public NPCConversation myConversation;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player") && myConversation != null)
        {
            // Start the conversation
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
}
