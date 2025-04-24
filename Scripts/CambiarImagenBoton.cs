using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CambiarImagenBoton  : MonoBehaviour
{
   [Header("Nueva imagen para este botón")]
    public Sprite nuevaImagen;

    [Header("Botones que reinician la imagen")]
    public List<Button> botonesReset;

    private Button botonActual;
    private Image imagenDelBoton;
    private Sprite imagenOriginal;

    void Start()
    {
        // Obtener componentes del botón
        botonActual = GetComponent<Button>();
        imagenDelBoton = GetComponent<Image>();

        // Validación
        if (botonActual == null || imagenDelBoton == null)
        {
            Debug.LogWarning("Este objeto debe tener componentes Button e Image.");
            return;
        }

        // Guardar imagen original
        imagenOriginal = imagenDelBoton.sprite;

        // Asignar listener para cambiar imagen
        botonActual.onClick.AddListener(CambiarImagen);

        // Asignar listeners a los botones reset
        foreach (Button b in botonesReset)
        {
            if (b != null)
                b.onClick.AddListener(RestaurarImagenOriginal);
        }
    }

    void CambiarImagen()
    {
        if (nuevaImagen != null)
        {
            imagenDelBoton.sprite = nuevaImagen;
            Debug.Log("Imagen cambiada.");
        }
    }

    void RestaurarImagenOriginal()
    {
        if (imagenDelBoton != null)
        {
            imagenDelBoton.sprite = imagenOriginal;
            Debug.Log("Imagen original restaurada.");
        }
    }
}
