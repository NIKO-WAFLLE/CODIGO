using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambioBotonHome : MonoBehaviour
{
    [Header("Imagen cuando está activo (cuando se presiona este botón o botones activadores)")]
    public Sprite imagenActiva;

    [Header("Imagen cuando otro botón es presionado (reseteo)")]
    public Sprite imagenInactiva;

    [Header("Botones que activan la imagen activa")]
    public List<Button> botonesQueActivan;

    [Header("Botones que activan la imagen inactiva")]
    public List<Button> botonesQueResetean;

    private Button miBoton;
    private Image miImagen;

    void Start()
    {
        miBoton = GetComponent<Button>();
        miImagen = GetComponent<Image>();

        if (miBoton != null)
            miBoton.onClick.AddListener(CambiarAActiva);

        foreach (Button b in botonesQueActivan)
        {
            if (b != null)
                b.onClick.AddListener(CambiarAActiva);
        }

        foreach (Button b in botonesQueResetean)
        {
            if (b != null)
                b.onClick.AddListener(CambiarAInactiva);
        }
    }

    void CambiarAActiva()
    {
        if (miImagen != null && imagenActiva != null)
        {
            miImagen.sprite = imagenActiva;
            Debug.Log("Imagen activada.");
        }
    }

    void CambiarAInactiva()
    {
        if (miImagen != null && imagenInactiva != null)
        {
            miImagen.sprite = imagenInactiva;
            Debug.Log("Imagen inactiva (reseteada).");
        }
    }
}