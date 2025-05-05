using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<MaterialItem> materials = new List<MaterialItem>();

    public bool HasMaterial(MaterialItem item)
    {
        return materials.Contains(item);
    }
}
