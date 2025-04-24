using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // ← Nuevo Input System

public class RotateWithTouch : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private bool isTouchingObject = false;
    private Vector2 lastTouchPosition;

    void Update()
    {
        if (Touchscreen.current == null || Touchscreen.current.primaryTouch.press.isPressed == false)
            return;

        Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
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
        else if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved && isTouchingObject)
        {
            Vector2 delta = touchPos - lastTouchPosition;
            transform.Rotate(delta.y * rotationSpeed * Time.deltaTime, -delta.x * rotationSpeed * Time.deltaTime, delta.x * rotationSpeed * Time.deltaTime, Space.World);
            lastTouchPosition = touchPos;
        }
        else if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            isTouchingObject = false;
        }
    }
}
