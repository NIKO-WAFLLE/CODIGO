using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchActivatePanel : MonoBehaviour
{
     [SerializeField] private GameObject panel; // Panel a activar
    [SerializeField] private Button closeButton; // Botón para cerrar el panel

    void Start()
    {
        if (panel != null)
            panel.SetActive(false); // Asegurarse de que el panel esté oculto al inicio
        
        if (closeButton != null)
            closeButton.onClick.AddListener(ClosePanel); // Asignar el evento al botón
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    panel.SetActive(true); // Activar el panel al tocar el objeto
                }
            }
        }
    }

    public void ClosePanel()
    {
        panel.SetActive(false); // Cierra el panel al presionar el botón
    }
}
