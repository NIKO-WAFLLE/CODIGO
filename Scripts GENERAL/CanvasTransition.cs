using UnityEngine;
using UnityEngine.UI;

public class CanvasTransition : MonoBehaviour
{
    public Canvas screenSpaceCanvas; // Canvas de tipo Screen Space
    public Canvas worldSpaceCanvas;  // Canvas de tipo World Space
    public Button transitionButton;  // Botón para iniciar la transición
    public float fadeDuration = 1f;  // Duración del desvanecimiento

    private CanvasGroup screenCanvasGroup;
    private CanvasGroup worldCanvasGroup;

    void Start()
    {
        // Obtener los CanvasGroup de ambos canvas
        screenCanvasGroup = screenSpaceCanvas.GetComponent<CanvasGroup>();
        worldCanvasGroup = worldSpaceCanvas.GetComponent<CanvasGroup>();

        // Asegurar que el World Space esté oculto al inicio
        worldCanvasGroup.alpha = 0f;
        worldCanvasGroup.interactable = false;
        worldCanvasGroup.blocksRaycasts = false;
        worldSpaceCanvas.gameObject.SetActive(false);

        // Listener del botón
        transitionButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        StartCoroutine(FadeOutScreenCanvas());
    }

    private System.Collections.IEnumerator FadeOutScreenCanvas()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            screenCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }

        screenCanvasGroup.alpha = 0f;
        screenCanvasGroup.interactable = false;
        screenCanvasGroup.blocksRaycasts = false;
        screenSpaceCanvas.gameObject.SetActive(false);

        StartCoroutine(FadeInWorldCanvas());
    }

    private System.Collections.IEnumerator FadeInWorldCanvas()
    {
        // Activar el canvas primero
        worldSpaceCanvas.gameObject.SetActive(true);

        float t = 0f;
        while (t < fadeDuration)
        {
            worldCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }

        worldCanvasGroup.alpha = 1f;
        worldCanvasGroup.interactable = true;
        worldCanvasGroup.blocksRaycasts = true;
    }
}
