using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CraftingUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject panel;
    public Image inputSlot1Image;
    public Image inputSlot2Image;
    public Image outputSlotImage;
    public TextMeshProUGUI holdToCraftText;
    public Button closeButton;

    [Header("Data")]
    public List<MaskRecipe> allRecipes;

    private MaterialItem inputMaterial1;
    private MaterialItem inputMaterial2;

    void Start()
    {
        panel.SetActive(false);
        holdToCraftText.gameObject.SetActive(false);
        closeButton.onClick.AddListener(HideCraftingUI);
        ClearInputs();
    }

    public void ShowCraftingUI()
    {
        panel.SetActive(true);
    }

    public void HideCraftingUI()
    {
        panel.SetActive(false);
        Time.timeScale = 1f; // resume game
        ClearInputs();
    }

    public void SetInputMaterial(MaterialItem item, int slotIndex)
    {
        if (slotIndex == 1)
        {
            inputMaterial1 = item;
            inputSlot1Image.sprite = item.icon;
            inputSlot1Image.color = Color.white;
        }
        else
        {
            inputMaterial2 = item;
            inputSlot2Image.sprite = item.icon;
            inputSlot2Image.color = Color.white;
        }

        CheckRecipeMatch();
    }

    private void CheckRecipeMatch()
    {
        outputSlotImage.sprite = null;
        outputSlotImage.color = new Color(1, 1, 1, 0); // Transparent

        if (inputMaterial1 == null || inputMaterial2 == null)
        {
            holdToCraftText.gameObject.SetActive(false);
            return;
        }

        foreach (var recipe in allRecipes)
        {
            bool match = (recipe.ingredient1 == inputMaterial1 && recipe.ingredient2 == inputMaterial2)
                      || (recipe.ingredient1 == inputMaterial2 && recipe.ingredient2 == inputMaterial1);

            if (match)
            {
                outputSlotImage.sprite = recipe.icon;
                outputSlotImage.color = Color.white;
                holdToCraftText.gameObject.SetActive(true);
                return;
            }
        }

        // No recipe found
        outputSlotImage.sprite = null;
        outputSlotImage.color = new Color(1, 1, 1, 0);
        holdToCraftText.gameObject.SetActive(false);
    }

    private void ClearInputs()
    {
        inputMaterial1 = null;
        inputMaterial2 = null;
        inputSlot1Image.sprite = null;
        inputSlot2Image.sprite = null;
        inputSlot1Image.color = new Color(1, 1, 1, 0);
        inputSlot2Image.color = new Color(1, 1, 1, 0);
        outputSlotImage.sprite = null;
        outputSlotImage.color = new Color(1, 1, 1, 0);
        holdToCraftText.gameObject.SetActive(false);
    }
}
