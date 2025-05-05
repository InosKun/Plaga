using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public MaskRecipe[] allRecipes;

    public MaterialItem input1;
    public MaterialItem input2;

    public Image resultIcon;
    public Text resultNameText;

    public Button craftButton;

    public float holdDuration = 2f;
    private float holdTimer = 0f;
    private bool canCraft = false;


    private void Start()
    {
        craftButton.onClick.AddListener(AttemptCraft);
    }

    public void SetInput(MaterialItem mat, int slot)
    {
        if (slot == 1) input1 = mat;
        else input2 = mat;

        CheckForMatch();
    }

    private void Update()
    {
        if (!canCraft) return;

        if (Input.GetKey(KeyCode.E))
        {
            holdTimer += Time.unscaledDeltaTime;

            if (holdTimer >= holdDuration)
            {
                AttemptCraft();
                holdTimer = 0f;
                canCraft = false;
            }
        }
        else
        {
            holdTimer = 0f;
        }
    }


    private void CheckForMatch()
    {
        foreach (var recipe in allRecipes)
        {
            if ((input1 == recipe.ingredient1 && input2 == recipe.ingredient2) ||
                (input1 == recipe.ingredient2 && input2 == recipe.ingredient1))
            {
                resultIcon.sprite = recipe.icon;
                resultNameText.text = recipe.maskName;
                craftButton.interactable = false; // We'll use hold instead
                canCraft = true;
                return;
            }
        }

        resultIcon.sprite = null;
        resultNameText.text = "";
        canCraft = false;
    }


    private void AttemptCraft()
    {
        foreach (var recipe in allRecipes)
        {
            if ((input1 == recipe.ingredient1 && input2 == recipe.ingredient2) ||
                (input1 == recipe.ingredient2 && input2 == recipe.ingredient1))
            {
                Debug.Log("Crafted: " + recipe.maskName);
                // TODO: Add mask to player state / UI
                ClearInputs();
                return;
            }
        }
    }

    private void ClearInputs()
    {
        input1 = null;
        input2 = null;
        resultIcon.sprite = null;
        resultNameText.text = "";
        craftButton.interactable = false;
    }
}
