using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CanvasGroup hoverOverlay;       // Drag the overlay image's CanvasGroup here
    public float fadeDuration = 0.2f;

    private Coroutine fadeCoroutine;

    private void Start()
    {
        if (hoverOverlay != null)
            hoverOverlay.alpha = 0f; // Start hidden
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartFade(1f); // Fade in
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartFade(0f); // Fade out
    }

    void StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeCanvasGroup(hoverOverlay, targetAlpha));
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float target)
    {
        float start = cg.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, target, time / fadeDuration);
            yield return null;
        }

        cg.alpha = target;
    }
}
