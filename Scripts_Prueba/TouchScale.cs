using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScale : MonoBehaviour
{
    [SerializeField] private Vector3 newScale = new Vector3(2f, 2f, 2f); // Escala deseada
    [SerializeField] private float transitionSpeed = 5f; // Velocidad de cambio
    private Vector3 originalScale;
    private bool isScaling = false;
    
    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    isScaling = !isScaling;
                }
            }
        }

        // Interpolamos suavemente entre la escala original y la nueva escala
        transform.localScale = Vector3.Lerp(transform.localScale, isScaling ? newScale : originalScale, Time.deltaTime * transitionSpeed);
    }
}
