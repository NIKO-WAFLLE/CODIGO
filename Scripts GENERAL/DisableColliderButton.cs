using UnityEngine;
using UnityEngine.UI;

public class DisableColliderButton : MonoBehaviour
{
   
    public Button disableButton; // Referencia al botón que desactiva el collider

    void Start()
    {
        // Si el botón está asignado, se le agrega el evento al hacer clic
        if (disableButton != null)
            disableButton.onClick.AddListener(DisableMyCollider);
    }

    /// <summary>
    /// Desactiva el componente Collider del objeto y oculta el botón.
    /// </summary>
    void DisableMyCollider()
    {
        // Obtener el collider del mismo GameObject
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false; // Desactivar el collider
            Debug.Log("Collider disabled.");
        }

        // Ocultar el botón después de usarlo
        if (disableButton != null)
            disableButton.gameObject.SetActive(false);
    }
}