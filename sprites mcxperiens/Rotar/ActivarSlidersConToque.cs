using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ActivarSlidersConToque : MonoBehaviour
{
    [Header("Sliders a activar/desactivar")]
    public List<GameObject> slidersParaControlar;

    private Camera cam;
    private bool slidersActivos = false;

    void Start()
    {
        cam = Camera.main;
        CambiarEstadoSliders(false);
    }

    void Update()
    {
        if (Touchscreen.current == null || !Touchscreen.current.primaryTouch.press.isPressed)
            return;

        var touch = Touchscreen.current.primaryTouch;
        Vector2 touchPos = touch.position.ReadValue();

        if (touch.press.wasPressedThisFrame)
        {
            bool tocoModelo = TocoEsteObjeto(touchPos);
            bool tocoSlider = TocoUnSlider(touchPos);

            // Mostrar sliders si tocó el objeto 3D
            if (tocoModelo)
            {
                CambiarEstadoSliders(true);
                slidersActivos = true;
                return;
            }

            // Ocultar sliders si NO tocó el modelo y NO tocó ningún slider
            if (!tocoModelo && !tocoSlider && slidersActivos)
            {
                CambiarEstadoSliders(false);
                slidersActivos = false;
            }
        }
    }

    bool TocoEsteObjeto(Vector2 pantalla)
    {
        Ray ray = cam.ScreenPointToRay(pantalla);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform == transform || hit.transform.IsChildOf(transform);
        }
        return false;
    }

    bool TocoUnSlider(Vector2 pantalla)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = pantalla;

        List<RaycastResult> resultados = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, resultados);

        foreach (var result in resultados)
        {
            foreach (var sliderGO in slidersParaControlar)
            {
                if (result.gameObject == sliderGO || result.gameObject.transform.IsChildOf(sliderGO.transform))
                {
                    return true; // Tocó un slider directamente o su hijo
                }
            }
        }

        return false;
    }

    void CambiarEstadoSliders(bool estado)
    {
        foreach (GameObject sliderGO in slidersParaControlar)
        {
            sliderGO.SetActive(estado);
        }
    }
}