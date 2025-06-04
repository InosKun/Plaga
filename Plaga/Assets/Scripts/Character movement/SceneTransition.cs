using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string targetScene;
    public bool useKeyInput = true; // If false, transition can be triggered manually
    public KeyCode interactionKey = KeyCode.E;
    private bool playerInRange = false;

    private void Update()
    {
        if (useKeyInput && playerInRange && Input.GetKeyDown(interactionKey))
        {
            LoadScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (useKeyInput && other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (useKeyInput && other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}

