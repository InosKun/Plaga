using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class CraftingUIManager : MonoBehaviour
{
    public static CraftingUIManager instance;

    public AudioClip materialInsertSound;
    public AudioClip craftingSuccessSound;
    public AudioSource audioSource;

    public CraftingSlot inputSlot1;
    public CraftingSlot inputSlot2;
    public Image outputSlotImage;
    public Button craftButton;

    public List<CraftingRecipe> recipes;

    private CraftingRecipe matchedRecipe;


    private void Awake()
    {
        instance = this;
        craftButton.onClick.AddListener(CraftItem);
        outputSlotImage.enabled = false;
    }

    public void OnSlotChanged()
    {
        matchedRecipe = null;

        // 🔊 Play material sound when a material is inserted
        if (audioSource != null && materialInsertSound != null)
        {
            audioSource.PlayOneShot(materialInsertSound);
        }

        // No item in both slots? Hide result
        if (inputSlot1.currentItem == null || inputSlot2.currentItem == null)
        {
            outputSlotImage.enabled = false;
            return;
        }

        // Try to find a matching recipe
        foreach (var recipe in recipes)
        {
            if ((recipe.ingredient1 == inputSlot1.currentItem && recipe.ingredient2 == inputSlot2.currentItem) ||
                (recipe.ingredient1 == inputSlot2.currentItem && recipe.ingredient2 == inputSlot1.currentItem))
            {
                matchedRecipe = recipe;
                break;
            }
        }

        if (matchedRecipe != null)
        {
            StartCoroutine(ShowOutputWithDelay());
        }
        else
        {
            outputSlotImage.sprite = null;
            outputSlotImage.enabled = false;
        }
    }


    void CraftItem()
    {
        if (matchedRecipe != null)
        {
            Debug.Log("Crafted: " + matchedRecipe.result.itemName);

            inputSlot1.Clear();
            inputSlot2.Clear();
            outputSlotImage.sprite = null;
            outputSlotImage.enabled = false;
        }
    }

    private IEnumerator ShowOutputWithDelay()
    {
        yield return new WaitForSeconds(0.3f); // Optional delay

        if (matchedRecipe != null)
        {
            outputSlotImage.sprite = matchedRecipe.result.icon;
            outputSlotImage.enabled = true;

            // 🔊 Play crafting success sound
            if (audioSource != null && craftingSuccessSound != null)
            {
                audioSource.PlayOneShot(craftingSuccessSound);
            }
        }
    }

}
