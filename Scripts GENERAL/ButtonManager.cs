using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    public Button ButtonLanguage;
    public Button ButtonLanguageOpen;
    public Button ButtonLanguageEspañol;
    public Button ButtonLanguageEnglish;
    public Button ButtonLanguageDeutsch;
    public Image IconEspañol;
    public Image IconEnglish;
    public Image IconDeutsch;
    public Button ButtonContinueEspañol;
    public Button ButtonContinueEnglish;
    public Button ButtonContinueDeutsch;
    public Button ButtonExit;

    public string NameEspañol;
    public string NameEnglish;
    public string NameDeutsch;

    // Fade
    public CanvasGroup canvasFade;
    public float fadeDuration = 1.5f;

    void Start()
    {
        ButtonLanguage.onClick.AddListener(ActivarBoton2);
        ButtonLanguageOpen.onClick.AddListener(ActivarBoton1);

        ButtonLanguageEspañol.onClick.AddListener(() => ActivarImagenYBoton(IconEspañol, ButtonContinueEspañol));
        ButtonLanguageEnglish.onClick.AddListener(() => ActivarImagenYBoton(IconEnglish, ButtonContinueEnglish));
        ButtonLanguageDeutsch.onClick.AddListener(() => ActivarImagenYBoton(IconDeutsch, ButtonContinueDeutsch));

        ButtonContinueEspañol.onClick.AddListener(() => StartCoroutine(FadeAndChangeScene(NameEspañol)));
        ButtonContinueEnglish.onClick.AddListener(() => StartCoroutine(FadeAndChangeScene(NameEnglish)));
        ButtonContinueDeutsch.onClick.AddListener(() => StartCoroutine(FadeAndChangeScene(NameDeutsch)));

        ButtonExit.onClick.AddListener(SalirAplicacion);

        // Inicialmente desactivar elementos
        ButtonLanguageOpen.gameObject.SetActive(false);
        IconEspañol.gameObject.SetActive(false);
        IconEnglish.gameObject.SetActive(false);
        IconDeutsch.gameObject.SetActive(false);
        ButtonContinueEspañol.gameObject.SetActive(false);
        ButtonContinueEnglish.gameObject.SetActive(false);
        ButtonContinueDeutsch.gameObject.SetActive(false);

        // Asegurar fade inicial
        if (canvasFade != null)
        {
            canvasFade.alpha = 0f;
            canvasFade.gameObject.SetActive(true);
        }
    }

    void ActivarBoton2()
    {
        ButtonLanguage.gameObject.SetActive(false);
        ButtonLanguageOpen.gameObject.SetActive(true);
    }

    void ActivarBoton1()
    {
        ButtonLanguageOpen.gameObject.SetActive(false);
        ButtonLanguage.gameObject.SetActive(true);
    }

    void ActivarImagenYBoton(Image imagen, Button botonExtra)
    {
        IconEspañol.gameObject.SetActive(false);
        IconEnglish.gameObject.SetActive(false);
        IconDeutsch.gameObject.SetActive(false);
        ButtonContinueEspañol.gameObject.SetActive(false);
        ButtonContinueEnglish.gameObject.SetActive(false);
        ButtonContinueDeutsch.gameObject.SetActive(false);

        imagen.gameObject.SetActive(true);
        botonExtra.gameObject.SetActive(true);
        ActivarBoton1();
    }

    IEnumerator FadeAndChangeScene(string sceneName)
    {
        if (canvasFade == null || string.IsNullOrEmpty(sceneName))
            yield break;

        float t = 0f;
        canvasFade.gameObject.SetActive(true);

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasFade.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        canvasFade.alpha = 1f;

        SceneManager.LoadScene(sceneName);
    }

    void SalirAplicacion()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
