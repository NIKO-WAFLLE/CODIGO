using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiodeEscena1 : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string nombreDeEscena;

    [Header("Botón que cambia de escena")]
    public Button botonCambiar;

    void Start()
    {
        if (botonCambiar != null)
            botonCambiar.onClick.AddListener(CargarEscena);
        else
            Debug.LogWarning("No se asignó el botón en el inspector.");
    }

    public void CargarEscena()
    {
        if (!string.IsNullOrEmpty(nombreDeEscena))
            SceneManager.LoadScene(nombreDeEscena);
        else
            Debug.LogError("El nombre de la escena está vacío.");
    }
}
