using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimacion10 : MonoBehaviour
{
    public Animator animacion; // Asegúrate de asignar el Animator en el Inspector de Unity
    public string nombreAnimacion = "escenario100"; // Nombre de la animación a reproducir
    public float tiempoDesactivacion = 5f; // Tiempo después del cual se desactiva la animación

    private bool puedeActivarAnimacion = true; // Flag para controlar la activación de animación

    private void Start()
    {
        if (animacion != null)
        {
            animacion.enabled = false; // Desactiva el componente Animator al inicio
            Debug.Log("Animator desactivado al inicio.");
        }
        else
        {
            Debug.LogError("Animator no asignado en " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el collider tiene el tag "Player" y si se puede activar la animación
        if (other.CompareTag("Player") && puedeActivarAnimacion)
        {
            if (animacion != null)
            {
                Debug.Log("Player ha colisionado con " + gameObject.name);
                animacion.enabled = true; // Activa el componente Animator
                animacion.Play(nombreAnimacion, 0, 0f); // Reproduce la animación desde el inicio
                Debug.Log("Animación '" + nombreAnimacion + "' reproducida en " + gameObject.name);
                StartCoroutine(DesactivarAnimacionDespuesDeTiempo(tiempoDesactivacion)); // Inicia la coroutine para desactivar la animación
                puedeActivarAnimacion = false; // Desactiva la posibilidad de activar la animación nuevamente
            }
            else
            {
                Debug.LogError("Animator no asignado en " + gameObject.name);
            }
        }
    }

    // Coroutine para desactivar la animación después de un tiempo
    private IEnumerator DesactivarAnimacionDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo); // Espera el tiempo especificado
        if (animacion != null)
        {
            animacion.enabled = false; // Desactiva el componente Animator
            Debug.Log("Animator desactivado después de " + tiempo + " segundos en " + gameObject.name);
        }
        puedeActivarAnimacion = true; // Permite volver a activar la animación
    }
}
