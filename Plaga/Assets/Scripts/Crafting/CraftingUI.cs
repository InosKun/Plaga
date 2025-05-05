using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f; // Resume the game
        }
    }
}

