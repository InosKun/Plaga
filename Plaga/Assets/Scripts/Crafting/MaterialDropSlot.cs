using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MaterialDropSlot : MonoBehaviour, IDropHandler
{
    public Image slotImage;
    public MaterialItem assignedMaterial;

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag?.GetComponent<DraggableMaterialUI>();
        if (dragged != null)
        {
            assignedMaterial = dragged.materialData;
            slotImage.sprite = dragged.GetComponent<Image>().sprite;

            // Snap visual into place
            dragged.transform.SetParent(transform);
            dragged.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

    public void ClearSlot()
    {
        assignedMaterial = null;
        slotImage.sprite = null;
    }

    public MaterialItem GetMaterial() => assignedMaterial;
}
