using UnityEngine;
using UnityEngine.UI;

public class CraftingInventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory; // your MonoBehaviour component
    public GameObject materialIconPrefab;
    public Transform contentParent; // The Content inside the ScrollView

    private void Start()
    {
        PopulateInventoryUI();
    }

    public void PopulateInventoryUI()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var material in playerInventory.materials)
        {
            GameObject icon = Instantiate(materialIconPrefab, contentParent);
            Image image = icon.GetComponent<Image>();
            //DraggableMaterialUI drag = icon.GetComponent<DraggableMaterialUI>();

            image.sprite = material.icon;
            //drag.materialData = material;
        }
    }
}

