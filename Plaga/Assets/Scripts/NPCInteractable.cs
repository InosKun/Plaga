using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public string npcName = "Mysterious Stranger";

    public void Interact()
    {
        Debug.Log("Talking to " + npcName);
    }
}

