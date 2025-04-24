using UnityEngine;
using System.Collections;

public class FadeStart : MonoBehaviour
{
    public CanvasGroup canvasFade;   // CanvasGroup asignado al Canvas de Fade
    public float fadeDuration = 1.5f; // Duración del fade

    void Start()
    {
        StartCoroutine(FadeOutOnStart());
    }

    IEnumerator FadeOutOnStart()
    {
        // Asegurarse de que el canvas esté activo y con alpha 1
        canvasFade.gameObject.SetActive(true);
        canvasFade.alpha = 1f;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasFade.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        canvasFade.alpha = 0f;
        canvasFade.gameObject.SetActive(false);
    }
}
