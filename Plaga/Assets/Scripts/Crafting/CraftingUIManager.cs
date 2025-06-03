using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CraftingUIManager : MonoBehaviour
{
    public static CraftingUIManager instance;

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

        if (inputSlot1.currentItem == null || inputSlot2.currentItem == null)
        {
            outputSlotImage.enabled = false;
            return;
        }

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
            outputSlotImage.sprite = matchedRecipe.result.icon;
            outputSlotImage.enabled = true;
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
}

