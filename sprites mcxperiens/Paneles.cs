using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paneles : MonoBehaviour
{
       [Header("Paneles a Activar")]
    public List<GameObject> panelesAActivar;

    [Header("Paneles a Desactivar")]
    public List<GameObject> panelesADesactivar;

    [Header("Botón que activa/desactiva")]
    public Button boton;

    private void Start()
    {
        if (boton != null)
        {
            boton.onClick.AddListener(TogglePaneles);
        }
        else
        {
            Debug.LogWarning("No se asignó ningún botón al script PanelToggle.");
        }
    }

    public void TogglePaneles()
    {
        foreach (GameObject panel in panelesAActivar)
        {
            if (panel != null)
                panel.SetActive(true);
        }

        foreach (GameObject panel in panelesADesactivar)
        {
            if (panel != null)
                panel.SetActive(false);
        }

        Debug.Log("Paneles actualizados por el botón: " + boton.name);
    }
}
