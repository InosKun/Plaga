using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Fade Visual")]
    public CanvasGroup hoverOverlay;
    public float fadeDuration = 0.2f;

    [Header("Hover Sound")]
    public AudioClip hoverSound;
    public AudioSource audioSource; // Optional: assign or it will add one automatically

    private Coroutine fadeCoroutine;

    private void Start()
    {
        if (hoverOverlay != null)
            hoverOverlay.alpha = 0f;

        if (audioSource == null && hoverSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartFade(1f);

        if (hoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartFade(0f);
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
