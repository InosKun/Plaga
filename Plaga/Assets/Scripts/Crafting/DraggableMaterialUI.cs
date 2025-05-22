using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableMaterialUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public MaterialItem materialData; // This is the ScriptableObject reference

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        Transform dragLayer = GameObject.Find("DragLayer").transform;
        transform.SetParent(dragLayer);

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        canvasGroup.blocksRaycasts = true;
    }
}
