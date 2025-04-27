using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Needed to listen for scene changes
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade instance;

    private Image fadeImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            fadeImage = GetComponentInChildren<Image>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Subscribe to scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe when disabled
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        FadeIn(0.5f); // Initial fade-in at the first scene
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Fade in every time a new scene loads
        FadeIn(0.5f);
    }

    public void FadeOut(float duration)
    {
        StartCoroutine(Fade(1f, duration));
    }

    public void FadeIn(float duration)
    {
        StartCoroutine(Fade(0f, duration));
    }

    private IEnumerator Fade(float targetAlpha, float duration)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0f;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}
