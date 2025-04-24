using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    [Header("Asigna 3 imágenes desde el inspector")]
    public Image[] images;

    [Header("Duraciones")]
    public float fadeDuration = 1f;
    public float visibleDuration = 3f;

    private void Start()
    {
        // Asegura que solo una imagen esté activa al inicio
        foreach (var img in images)
        {
            SetAlpha(img, 0f);
        }

        StartCoroutine(FadeLoop());
    }

    private IEnumerator FadeLoop()
    {
        while (true)
        {
            for (int i = 0; i < images.Length; i++)
            {
                yield return StartCoroutine(FadeIn(images[i]));
                yield return new WaitForSeconds(visibleDuration);
                yield return StartCoroutine(FadeOut(images[i]));
            }
        }
    }

    private IEnumerator FadeIn(Image image)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            SetAlpha(image, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeOut(Image image)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            SetAlpha(image, alpha);
            yield return null;
        }
    }

    private void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
