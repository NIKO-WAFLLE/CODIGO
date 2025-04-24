using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // ← Nuevo Input System

public class RotarConSlider : MonoBehaviour
{ [Header("Slider de rotación")]
    public Slider slider;

    [Header("Ángulo máximo de rotación en Y")]
    public float anguloMaximo = 180f;

    private float rotacionInicialY;

    void Start()
    {
        // Guarda la rotación original del objeto en Y
        rotacionInicialY = transform.eulerAngles.y;

        // Suscribirse al evento del slider
        if (slider != null)
            slider.onValueChanged.AddListener(RotarObjeto);
    }

    void RotarObjeto(float valor)
    {
        float nuevoAnguloY = Mathf.Lerp(0f, anguloMaximo, valor);
        Vector3 nuevaRotacion = new Vector3(
            transform.eulerAngles.x,
            rotacionInicialY + nuevoAnguloY,
            transform.eulerAngles.z
        );

        transform.eulerAngles = nuevaRotacion;
    }
}
