using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BOMBA : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);

            // Elimina todos los objetos con el tag "GameControler"
            GameObject[] objetosConTag = GameObject.FindGameObjectsWithTag("GameController");
            foreach (GameObject obj in objetosConTag)
            {
                Destroy(obj);
            }

            Debug.Log("Game over");

            FindAnyObjectByType<CAMBA>().MostrarGameOver();

            Debug.Log("mostrar pantalla muerte ");
        }
    }

}
