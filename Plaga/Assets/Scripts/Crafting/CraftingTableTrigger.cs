using UnityEngine;
using System.Collections;

public class CraftingTableTrigger : MonoBehaviour
{
    public GameObject craftingCanvas;
    public GameObject craftingPanel;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OpenCraftingUI());
        }
    }

    IEnumerator OpenCraftingUI()
    {
        craftingCanvas.SetActive(true);
        yield return null; // Wait one frame
        craftingPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
