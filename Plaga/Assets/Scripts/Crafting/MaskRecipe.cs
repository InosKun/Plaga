using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Mask Recipe")]
public class MaskRecipe : ScriptableObject
{
    public string maskName; // 🔴 This is the missing line
    public Sprite icon;

    public MaterialItem ingredient1;
    public MaterialItem ingredient2;
}
