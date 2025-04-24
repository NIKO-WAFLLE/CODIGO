using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CAMBA : MonoBehaviour
{
    public TMP_Text TextPuntos;

    public GameObject gameOverPanel;

    public void MostrarGameOver()
    {
        gameOverPanel.SetActive(true); // <- Esto activa el panel correctamente
        TextPuntos.text = "Puntaje: " + FindAnyObjectByType<Puntaje>().puntos.ToString();
    }

}
