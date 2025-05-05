using UnityEngine;

[CreateAssetMenu(fileName = "NewMaskRecipe", menuName = "Crafting/MaskRecipe")]
public class MaskRecipe : ScriptableObject
{
    public string maskName;
    public Sprite icon;
    public MaterialItem ingredient1;
    public MaterialItem ingredient2;
    public string description;
}
