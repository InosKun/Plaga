using UnityEngine;
using UnityEngine.UI;

public class UIFrameManager : MonoBehaviour
{
    public static UIFrameManager instance;

    public Image frameImage;                  // The UI image to swap
    public Sprite[] frameOptions;             // The list of alternate sprites
    public float transitionDuration = 0.3f;   // Time to fade out/in
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        instance = this;
        canvasGroup = frameImage.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = frameImage.gameObject.AddComponent<CanvasGroup>();
    }

    public void ChangeFrame(int index)
    {
        if (index < 0 || index >= frameOptions.Length)
        {
            Debug.LogWarning("Frame index out of bounds!");
            return;
        }

        // Fade out, change sprite, fade in
        LeanTween.alphaCanvas(canvasGroup, 0f, transitionDuration).setOnComplete(() =>
        {
            frameImage.sprite = frameOptions[index];
            LeanTween.alphaCanvas(canvasGroup, 1f, transitionDuration);
        });
    }
}

