using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruta : MonoBehaviour
{

    [SerializeField] private float cantidadPuntos;
    private Puntaje puntaje;

    private void Start()
    {
        puntaje = GameObject.FindObjectOfType<Puntaje>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (puntaje != null)
            {
                puntaje.SumarPuntos(cantidadPuntos);
            }

       
            Destroy(gameObject);
        }
    }
}
