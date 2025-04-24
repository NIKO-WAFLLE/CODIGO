using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayAnimation : MonoBehaviour
{
    public Canvas firstCanvas;
    public Canvas secondCanvas;
    public Animator animator;
    public string animationName;
    public Button firstButton;
    public Button secondButton;

    public AudioClip clickAudio; // Clip de audio a reproducir

    private AudioSource audioSource; // Fuente de audio para reproducir el clip
    private CanvasGroup firstCanvasGroup;
    private CanvasGroup secondCanvasGroup;

    private float animationDuration;
    private float frameDuration;

    private void Start()
    {
        // Agregar AudioSource automáticamente si no existe
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; // 2D
            audioSource.volume = 1f;
        }

        // Obtener o agregar CanvasGroup al primer canvas
        firstCanvasGroup = firstCanvas.GetComponent<CanvasGroup>();
        if (firstCanvasGroup == null) firstCanvasGroup = firstCanvas.gameObject.AddComponent<CanvasGroup>();

        // Obtener o agregar CanvasGroup al segundo canvas
        secondCanvasGroup = secondCanvas.GetComponent<CanvasGroup>();
        if (secondCanvasGroup == null) secondCanvasGroup = secondCanvas.gameObject.AddComponent<CanvasGroup>();

        // Ocultar el segundo canvas al inicio
        SetCanvasAlpha(secondCanvasGroup, 0f);
        secondCanvas.gameObject.SetActive(false);

        // Asignar eventos a los botones
        firstButton.onClick.AddListener(OnFirstButtonClick);
        secondButton.onClick.AddListener(OnSecondButtonClick);

        // Obtener detalles de la animación
        SetupAnimationDetails();
    }

    private void SetupAnimationDetails()
    {
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;

        foreach (AnimationClip clip in controller.animationClips)
        {
            if (clip.name == animationName)
            {
                animationDuration = clip.length;
                frameDuration = 1f / clip.frameRate;
                break;
            }
        }
    }

    private void OnFirstButtonClick()
    {
        // Reproducir el audio si se ha asignado
        if (clickAudio != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickAudio);
        }

        // Ocultar el primer canvas y comenzar la animación
        StartCoroutine(FadeCanvas(firstCanvasGroup, 1f, 0f, 1f));
        StartCoroutine(PlayAnimationAfterDelay(1f));
    }

    private void OnSecondButtonClick()
    {
        animator.Play(animationName, 0, 0f);
    }

    private IEnumerator PlayAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        animator.Play(animationName);
        yield return new WaitForSeconds(animationDuration);
        yield return new WaitForSeconds(1f);

        secondCanvas.gameObject.SetActive(true);
        StartCoroutine(FadeCanvas(secondCanvasGroup, 0f, 1f, 1f));
    }

    private IEnumerator FadeCanvas(CanvasGroup canvasGroup, float alphaStart, float alphaEnd, float fadeTime)
    {
        float time = 0f;
        canvasGroup.alpha = alphaStart;

        while (time < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(alphaStart, alphaEnd, time / fadeTime);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = alphaEnd;
    }

    private void SetCanvasAlpha(CanvasGroup canvasGroup, float alpha)
    {
        canvasGroup.alpha = alpha;
    }
}