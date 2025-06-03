using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public MaterialItem ingredient1;
    public MaterialItem ingredient2;
    public MaterialItem result;
}
