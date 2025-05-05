using UnityEngine;

[CreateAssetMenu(fileName = "NewMaterial", menuName = "Crafting/Material")]
public class MaterialItem : ScriptableObject
{
    public string materialName;
    public Sprite icon;
}

