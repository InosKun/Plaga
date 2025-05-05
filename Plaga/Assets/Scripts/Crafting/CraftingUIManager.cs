using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUIManager : MonoBehaviour
{
    public GameObject panel;
    public Image inputSlot1Image;
    public Image inputSlot2Image;
    public Image outputSlotImage;
    public TextMeshProUGUI holdToCraftText;
    public Button closeButton;

    void Start()
    {
        panel.SetActive(false);
        holdToCraftText.gameObject.SetActive(false);
        closeButton.onClick.AddListener(() => panel.SetActive(false));
    }

    public void ShowCraftingUI()
    {
        panel.SetActive(true);
    }

    public void HideCraftingUI()
    {
        panel.SetActive(false);
    }
}
