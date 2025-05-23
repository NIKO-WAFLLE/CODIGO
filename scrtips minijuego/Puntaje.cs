using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    public float puntos;
    public float tiempo;
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        tiempo += Time.deltaTime; 
        textMesh.text = puntos.ToString("0");
    }
    
    public void SumarPuntos(float PuntosEntrada)
    {
        puntos += PuntosEntrada;
    }
}
