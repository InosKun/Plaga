using UnityEngine;
using TMPro;

public class ThoughtBubbleUI : MonoBehaviour
{
    public static ThoughtBubbleUI instance;

    public RectTransform bubbleRoot;
    public TextMeshProUGUI bubbleText;
    public float displayDuration = 3f;

    private Transform followTarget;
    private CanvasGroup canvasGroup;

    public Vector3 worldOffset = new Vector3(0, 0.8f, 0); // customizable offset

    private void Awake()
    {
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        HideBubbleInstant();
    }

    private void Update()
    {
        if (followTarget != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(followTarget.position + worldOffset);
            bubbleRoot.position = screenPos;
        }
    }

    public void ShowThought(string message, Transform target)
    {
        followTarget = target;
        bubbleText.text = message;
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        LeanTween.alphaCanvas(canvasGroup, 1f, 0.3f);

        CancelInvoke(nameof(HideBubble));
        Invoke(nameof(HideBubble), displayDuration);
    }

    private void HideBubble()
    {
        LeanTween.alphaCanvas(canvasGroup, 0f, 0.3f).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    private void HideBubbleInstant()
    {
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
