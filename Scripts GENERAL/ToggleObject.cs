using UnityEngine;
using UnityEngine.UI;

public class ToggleObject : MonoBehaviour
{
    public GameObject targetObject; // El objeto a activar/desactivar
    public Button activateButton;
    public Button deactivateButton;

    void Start()
    {
        // Asigna las funciones a los botones
        activateButton.onClick.AddListener(ActivateObject);
        deactivateButton.onClick.AddListener(DeactivateObject);
    }

    void ActivateObject()
    {
        targetObject.SetActive(true);
    }

    void DeactivateObject()
    {
        targetObject.SetActive(false);
    }
}
