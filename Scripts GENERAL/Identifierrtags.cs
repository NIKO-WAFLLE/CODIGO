using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Identifierrtags : MonoBehaviour
{
    public Animator animator;      // Referencia al Animator
    public string animationName;   // Nombre de la animación que esperamos
    public GameObject emptyObject; // El Empty que se activará/desactivará
    public GameObject otherObject; // El nuevo GameObject que se activará/desactivará
    public Button deactivateButton; // El botón que desactivará los objetos cuando se presione

    private bool hasAnimationFinished = false;

    void Start()
    {
        // Asegurarnos de que el Empty y el otro GameObject estén desactivados al inicio
        if (emptyObject != null)
            emptyObject.SetActive(false);

        if (otherObject != null)
            otherObject.SetActive(false);

        // Si el botón está asignado, asignamos la función de desactivación al evento onClick
        if (deactivateButton != null)
            deactivateButton.onClick.AddListener(DeactivateObjects);
    }

    void Update()
    {
        // Comprobamos si la animación correcta está en ejecución
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            // Verificamos si la animación ha terminado
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            float animationProgress = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

            if (animationProgress >= 0.99f && !hasAnimationFinished)
            {
                // Iniciamos la corutina para activar los objetos 1 segundo después de terminar la animación
                StartCoroutine(ActivateObjectsWithDelay(1f));
                hasAnimationFinished = true;
            }
        }
        else
        {
            // Si la animación no es la que esperamos, reseteamos la lógica de fin de animación
            hasAnimationFinished = false;
        }
    }

    // Corutina que espera un segundo antes de activar los objetos
    IEnumerator ActivateObjectsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado (1 segundo en este caso)
        ActivateObjects(true); // Activa los objetos
    }

    // Método que se llama cuando el botón es presionado para desactivar los objetos
    void DeactivateObjects()
    {
        ActivateObjects(false); // Desactivamos los objetos
    }

    void ActivateObjects(bool activate)
    {
        if (emptyObject != null)
            emptyObject.SetActive(activate);

        if (otherObject != null)
            otherObject.SetActive(activate);
    }
}
