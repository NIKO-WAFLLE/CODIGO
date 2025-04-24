using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    // Paneles
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    // Sliders
    public Slider ySlider;
    public Slider scaleSlider;

    // Canvas principal en WorldSpace
    public GameObject mainCanvas;

    // Canvas de fade
    public CanvasGroup canvasFade;
    public float fadeDuration = 1f;

    // Paneles como RectTransforms para moverlos
    private RectTransform panel1RectTransform;
    private RectTransform panel2RectTransform;
    private RectTransform panel3RectTransform;

    // Botones
    public Button buttonPanel1;
    public Button buttonPanel2;
    public Button buttonPanel3;
    public Button buttonActivateMainCanvas;
    public Button buttonDeactivateMainCanvas;
    public Button buttonExit;

    void Start()
    {
        // Asignamos los RectTransforms de los paneles
        panel1RectTransform = panel1.GetComponent<RectTransform>();
        panel2RectTransform = panel2.GetComponent<RectTransform>();
        panel3RectTransform = panel3.GetComponent<RectTransform>();

        // Asignar los eventos de los botones
        buttonPanel1.onClick.AddListener(ShowPanel1);
        buttonPanel2.onClick.AddListener(ShowPanel2);
        buttonPanel3.onClick.AddListener(ShowPanel3);
        buttonActivateMainCanvas.onClick.AddListener(ActivateMainCanvas);
        buttonDeactivateMainCanvas.onClick.AddListener(DeactivateMainCanvas);
        buttonExit.onClick.AddListener(ExitApplication);

        // Asignar los sliders
        ySlider.onValueChanged.AddListener(MovePanels);
        scaleSlider.onValueChanged.AddListener(ScalePanels);
    }

    void ShowPanel1()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
    }

    void ShowPanel2()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
        panel3.SetActive(false);
    }

    void ShowPanel3()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);
    }

    void MovePanels(float value)
    {
        // Mover los paneles en el eje Y con el valor del slider
        float yPos = Mathf.Lerp(-300f, 300f, value);

        panel1RectTransform.anchoredPosition = new Vector2(panel1RectTransform.anchoredPosition.x, yPos);
        panel2RectTransform.anchoredPosition = new Vector2(panel2RectTransform.anchoredPosition.x, yPos);
        panel3RectTransform.anchoredPosition = new Vector2(panel3RectTransform.anchoredPosition.x, yPos);
    }

    void ScalePanels(float value)
    {
        // Escalar los paneles entre 1 y 2 según el slider
        float scaleValue = Mathf.Lerp(1f, 2f, value);

        panel1RectTransform.localScale = new Vector3(scaleValue, scaleValue, 1);
        panel2RectTransform.localScale = new Vector3(scaleValue, scaleValue, 1);
        panel3RectTransform.localScale = new Vector3(scaleValue, scaleValue, 1);
    }

    void ActivateMainCanvas()
    {
        StartCoroutine(FadeInThenShowMainCanvas());
    }

    void DeactivateMainCanvas()
    {
        StartCoroutine(FadeInThenHideMainCanvas());
    }

    void ExitApplication()
    {
        // Salir de la aplicación
        Application.Quit();

        // Si estás en el editor de Unity, puedes usar esta línea para detener la ejecución
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    IEnumerator FadeInThenShowMainCanvas()
    {
        canvasFade.gameObject.SetActive(true);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasFade.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        canvasFade.alpha = 1f;

        mainCanvas.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasFade.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        canvasFade.alpha = 0f;
        canvasFade.gameObject.SetActive(false);
    }

    IEnumerator FadeInThenHideMainCanvas()
    {
        canvasFade.gameObject.SetActive(true);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasFade.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        canvasFade.alpha = 1f;

        mainCanvas.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        t = 0f;
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
