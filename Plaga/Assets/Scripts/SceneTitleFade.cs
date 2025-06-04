using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTitleFade : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeInTime = 1f;
    public float displayTime = 2f;
    public float fadeOutTime = 1f;

    private void Start()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        // Fade in
        yield return StartCoroutine(Fade(0f, 1f, fadeInTime));

        // Wait
        yield return new WaitForSeconds(displayTime);

        // Fade out
        yield return StartCoroutine(Fade(1f, 0f, fadeOutTime));

        // Optional: Disable image after fade
        gameObject.SetActive(false);
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, timer / duration);
            yield return null;
        }

        canvasGroup.alpha = to;
    }
}
