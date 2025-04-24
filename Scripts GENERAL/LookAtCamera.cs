using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Referencia a la cámara principal
    private Camera mainCamera;

    // Método llamado al inicio del juego
    void Start()
    {
        // Obtiene la cámara principal del escenario
        mainCamera = Camera.main; // Camera.main devuelve la primera cámara etiquetada como "MainCamera"
    }

    // Método llamado cada cuadro para actualizar la orientación del objeto
    void Update()
    {
        // Si la cámara principal ha sido encontrada
        if (mainCamera != null)
        {
            // Hace que el objeto siempre mire hacia la cámara
            // La dirección se obtiene sumando la posición del objeto y la dirección hacia adelante de la cámara
            // Utiliza el vector hacia adelante de la cámara (Camera.main.transform.rotation * Vector3.forward)
            // y el vector hacia arriba de la cámara (Camera.main.transform.rotation * Vector3.up)
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);
        }
    }
}
