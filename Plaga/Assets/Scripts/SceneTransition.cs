using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene changing

public class SceneTransition : MonoBehaviour
{
    [Header("Scene Transition Settings")]
    public string sceneToLoad; // Name of the scene to load when interacting

    private bool playerInRange = false;

    [Header("Visual Cue")]
    public GameObject visualCue; // Assign a child object with a visual hint

    [Header("Spawn Settings")]
    public string targetSpawnID; // ID of the SpawnPoint where the player will appear


    private void Start()
    {
        if (visualCue != null)
            visualCue.SetActive(false); // Hide the cue by default
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Transition());

            }
        }
    }

    private IEnumerator Transition()
    {
        SpawnManager.nextSpawnPointID = targetSpawnID; // Save the ID where to spawn next
        ScreenFade.instance.FadeOut(0.5f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneToLoad);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (visualCue != null)
                visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (visualCue != null)
                visualCue.SetActive(false);
        }
    }
}
