using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("Scene Transition Settings")]
    public string sceneToLoad;
    public string targetSpawnID;

    [Header("Use Key Input or Manual Call")]
    public bool useKeyInput = true;
    public KeyCode interactionKey = KeyCode.E;

    [Header("Optional Visual Cue")]
    public GameObject visualCue;

    private bool playerInRange = false;

    private void Start()
    {
        if (visualCue != null)
            visualCue.SetActive(false);
    }

    private void Update()
    {
        if (useKeyInput && playerInRange && Input.GetKeyDown(interactionKey))
        {
            StartCoroutine(Transition());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (useKeyInput && other.CompareTag("Player"))
        {
            playerInRange = true;
            if (visualCue != null)
                visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (useKeyInput && other.CompareTag("Player"))
        {
            playerInRange = false;
            if (visualCue != null)
                visualCue.SetActive(false);
        }
    }

    /// <summary>
    /// Call this from a UI Button to trigger scene load
    /// </summary>
    public void LoadSceneFromButton()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        // Save target spawn point
        SpawnManager.nextSpawnPointID = targetSpawnID;

        // Fade out
        if (ScreenFade.instance != null)
        {
            ScreenFade.instance.FadeOut(0.5f);
            yield return new WaitForSeconds(0.5f);
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}

