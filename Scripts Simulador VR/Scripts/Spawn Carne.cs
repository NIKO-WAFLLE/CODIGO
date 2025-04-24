using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCarne : MonoBehaviour
{
    public Transform puntoDeAparicion; // Punto de aparición asignado desde el Inspector
    public GameObject objetoAparecer; // Objeto que aparecerá asignado desde el Inspector
    public float tiempoReactivacion = 5f; // Tiempo después del cual se puede instanciar nuevamente

    private GameObject objetoInstanciado; // Referencia al objeto instanciado
    private bool puedeInstanciar = true; // Flag para controlar la instanciación del objeto

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el collider tiene el tag "Player" y si se puede instanciar
        if (other.CompareTag("Player") && puedeInstanciar)
        {
            // Verifica si el punto de aparición y el objeto están asignados
            if (puntoDeAparicion != null && objetoAparecer != null)
            {
                // Instancia el objeto en el punto de aparición y guarda la referencia
                objetoInstanciado = Instantiate(objetoAparecer, puntoDeAparicion.position, puntoDeAparicion.rotation);

                // Desactiva el Animator del objeto instanciado
                Animator animador = objetoInstanciado.GetComponent<Animator>();
                if (animador != null)
                {
                    animador.enabled = false;
                }

                Debug.Log("Objeto " + objetoAparecer.name + " apareció en " + puntoDeAparicion.position);

                // Desactiva la posibilidad de instanciar otro objeto hasta que pase el tiempo de reactivación
                puedeInstanciar = false;
                StartCoroutine(ReactivarInstanciacionDespuesDeTiempo(tiempoReactivacion));
            }
            else
            {
                if (puntoDeAparicion == null)
                {
                    Debug.LogError("Punto de aparición no asignado en " + gameObject.name);
                }
                if (objetoAparecer == null)
                {
                    Debug.LogError("Objeto a aparecer no asignado en " + gameObject.name);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que salió del trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Aquí no es necesario hacer nada si la reactivación se basa en tiempo
        }
    }

    // Coroutine para reactivar la posibilidad de instanciar después de un tiempo
    private IEnumerator ReactivarInstanciacionDespuesDeTiempo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        puedeInstanciar = true;
        Debug.Log("La instanciación de objetos está reactivada.");
    }

    // Método para obtener la referencia al objeto instanciado
    public GameObject ObtenerObjetoInstanciado()
    {
        return objetoInstanciado;
    }
}
