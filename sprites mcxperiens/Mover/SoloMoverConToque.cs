using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SoloMoverConToque : MonoBehaviour
{
     [Header("Velocidad de movimiento")]
    public float moveSpeed = 0.005f;

    [Header("Límites de Movimiento (X, Y, Z)")]
    public Vector3 minPosition = new Vector3(-1f, 0f, -1f);
    public Vector3 maxPosition = new Vector3(1f, 2f, 1f);

    private bool isTouchingObject = false;
    private Vector2 lastTouchPosition;

    void Update()
    {
        if (Touchscreen.current == null || Touchscreen.current.primaryTouch.press.isPressed == false)
            return;

        var touch = Touchscreen.current.primaryTouch;
        Vector2 touchPos = touch.position.ReadValue();

        // Cuando inicia el toque
        if (touch.press.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                isTouchingObject = true;
                lastTouchPosition = touchPos;
            }
            else
            {
                isTouchingObject = false;
            }
        }

        // Mientras arrastra el dedo
        else if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved && isTouchingObject)
        {
            Vector2 delta = touchPos - lastTouchPosition;
            Vector3 movement = new Vector3(delta.x * moveSpeed, delta.y * moveSpeed, 0f);
            Vector3 newPosition = transform.position + movement;

            // Limita el movimiento
            newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);
            newPosition.z = Mathf.Clamp(newPosition.z, minPosition.z, maxPosition.z);

            transform.position = newPosition;

            lastTouchPosition = touchPos;
        }

        // Cuando se suelta el dedo
        else if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            isTouchingObject = false;
        }
    }
}
