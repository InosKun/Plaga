using UnityEngine;
using System.Collections.Generic;

public class NPCInteractable : MonoBehaviour
{
    public string npcName = "Mysterious Stranger";
    public List<DialogueLine> dialogueLines;

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered NPC trigger!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited NPC trigger!");
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E pressed while near NPC.");

                if (!DialogueManager.instance.IsDialogueActive())
                {
                    Debug.Log("Starting dialogue.");
                    DialogueManager.instance.StartDialogue(dialogueLines);
                }
            }
        }
    }

}

