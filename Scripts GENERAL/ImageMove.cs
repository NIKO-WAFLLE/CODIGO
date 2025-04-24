using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageMover : MonoBehaviour
{
    [Header("Referencias")]
    public Transform destination; // Objeto de destino (se usará solo su posición en Y)
    public float moveDuration = 1f; // Duración del movimiento en el eje Y

    [Header("Imágenes a mover (10)")]
    public Transform[] images; // Arreglo con las 10 imágenes a mover

    [Header("Botones (10)")]
    public Button[] buttons; // Arreglo con los 10 botones que activan el movimiento

    private Vector3[] originalPositions; // Almacena las posiciones originales de las imágenes
    private int currentIndex = -1; // Índice de la imagen actualmente en el destino
    private Coroutine currentMovement; // Referencia a la corrutina en curso (si hay una)

    void Start()
    {
        // Guardar la posición original de cada imagen
        originalPositions = new Vector3[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            originalPositions[i] = images[i].position;
        }

        // Asignar la función al evento onClick de cada botón
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Importante: capturar el índice correctamente
            buttons[i].onClick.AddListener(() => MoveImage(index));
        }
    }

    /// <summary>
    /// Inicia el movimiento de la imagen correspondiente al botón presionado.
    /// </summary>
    /// <param name="newIndex">Índice de la imagen a mover</param>
    public void MoveImage(int newIndex)
    {
        // Si ya hay un movimiento en proceso, detenerlo
        if (currentMovement != null)
            StopCoroutine(currentMovement);

        // Iniciar nueva corrutina para el movimiento
        currentMovement = StartCoroutine(MoveImageCoroutine(newIndex));
    }

    /// <summary>
    /// Corrutina que maneja el movimiento de la imagen actual de regreso y el movimiento de la nueva imagen al destino.
    /// </summary>
    IEnumerator MoveImageCoroutine(int newIndex)
    {
        // Si hay una imagen en el destino y es diferente a la nueva, regresarla a su posición original en Y
        if (currentIndex != -1 && currentIndex != newIndex)
        {
            Vector3 targetBackPosition = new Vector3(
                images[currentIndex].position.x,
                originalPositions[currentIndex].y,
                images[currentIndex].position.z
            );

            yield return StartCoroutine(SmoothMove(images[currentIndex], targetBackPosition));
        }

        // Mover la nueva imagen a la posición del destino (solo eje Y)
        Vector3 targetYPosition = new Vector3(
            images[newIndex].position.x,
            destination.position.y,
            images[newIndex].position.z
        );

        yield return StartCoroutine(SmoothMove(images[newIndex], targetYPosition));
        currentIndex = newIndex; // Actualizar el índice actual
    }

    /// <summary>
    /// Corrutina que mueve suavemente una imagen hacia una posición objetivo.
    /// </summary>
    IEnumerator SmoothMove(Transform image, Vector3 targetPosition)
    {
        Vector3 start = image.position;
        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            Vector3 newPos = Vector3.Lerp(start, targetPosition, time / moveDuration);
            image.position = newPos;
            yield return null;
        }

        image.position = targetPosition; // Asegura la posición final exacta
    }
}
