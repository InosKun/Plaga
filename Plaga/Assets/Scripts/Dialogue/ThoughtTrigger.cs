using UnityEngine;

public class ThoughtTrigger : MonoBehaviour
{
    [TextArea(2, 4)]
    public string thoughtText;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            ThoughtBubbleUI.instance.ShowThought(thoughtText, other.transform);
            triggered = true;
        }
    }
}

