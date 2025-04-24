using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // ← Nuevo Input System

public class RotarEnXConSlider : MonoBehaviour
{
     [Header("Slider de rotación")]
    public Slider slider;

    [Header("Ángulo máximo de rotación en X")]
    public float anguloMaximo = 180f;

    private float anguloBaseY;
    private float anguloBaseZ;

    void Start()
    {
        // Guardamos los ángulos Y y Z actuales para mantenerlos fijos
        anguloBaseY = transform.eulerAngles.y;
        anguloBaseZ = transform.eulerAngles.z;

        if (slider != null)
            slider.onValueChanged.AddListener(RotarObjeto);
    }

    void RotarObjeto(float valor)
    {
        // Rota solo en X, el valor va de 0 a 1
        float anguloX = Mathf.Lerp(0f, anguloMaximo, valor);

        transform.rotation = Quaternion.Euler(anguloX, anguloBaseY, anguloBaseZ);
    }
}
