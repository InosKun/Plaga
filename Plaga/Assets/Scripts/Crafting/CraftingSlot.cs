using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour, IDropHandler
{
    public Image iconDisplay;
    public MaterialItem currentItem;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem dragged = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (dragged != null)
        {
            currentItem = dragged.itemData;
            iconDisplay.sprite = currentItem.icon;
            iconDisplay.enabled = true;

            CraftingUIManager.instance.OnSlotChanged();
        }
    }

    public void Clear()
    {
        currentItem = null;
        iconDisplay.sprite = null;
        iconDisplay.enabled = false;
    }
}
