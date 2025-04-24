using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveAndRotateWithTouch : MonoBehaviour
{
    [Header("Velocidades")]
    public float moveSpeed = 0.005f;
    public float rotationSpeed = 0.2f;

    [Header("Límites de Movimiento (X, Y, Z)")]
    public Vector3 minPosition = new Vector3(-1f, 0f, -1f);
    public Vector3 maxPosition = new Vector3(1f, 2f, 1f);

    private Vector2 lastTouchPosition1;
    private Vector2 lastTouchPosition2;
    private bool isTouching = false;

    void Update()
    {
        if (Touchscreen.current == null || Touchscreen.current.touches.Count == 0)
            return;

        int touchCount = Touchscreen.current.touches.Count;

        if (touchCount == 1)
        {
            var touch = Touchscreen.current.primaryTouch;

            if (touch.press.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    isTouching = true;
                    lastTouchPosition1 = touch.position.ReadValue();
                }
            }
            else if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved && isTouching)
            {
                Vector2 delta = touch.position.ReadValue() - lastTouchPosition1;

                Vector3 move = new Vector3(delta.x * moveSpeed, delta.y * moveSpeed, 0f);
                Vector3 newPosition = transform.position + move;

                // Limita movimiento
                newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
                newPosition.y = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);
                newPosition.z = Mathf.Clamp(newPosition.z, minPosition.z, maxPosition.z);

                transform.position = newPosition;
                lastTouchPosition1 = touch.position.ReadValue();
            }
            else if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                isTouching = false;
            }
        }
        else if (touchCount >= 2)
        {
            var touch1 = Touchscreen.current.touches[0];
            var touch2 = Touchscreen.current.touches[1];

            if (touch1.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved ||
                touch2.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                Vector2 currPos1 = touch1.position.ReadValue();
                Vector2 currPos2 = touch2.position.ReadValue();

                Vector2 delta1 = currPos1 - lastTouchPosition1;
                Vector2 delta2 = currPos2 - lastTouchPosition2;

                Vector2 averageDelta = (delta1 + delta2) * 0.5f;

                transform.Rotate(Vector3.up, -averageDelta.x * rotationSpeed, Space.World);
                transform.Rotate(Vector3.right, averageDelta.y * rotationSpeed, Space.World);

                lastTouchPosition1 = currPos1;
                lastTouchPosition2 = currPos2;
            }
            else if (touch1.press.wasPressedThisFrame || touch2.press.wasPressedThisFrame)
            {
                lastTouchPosition1 = touch1.position.ReadValue();
                lastTouchPosition2 = touch2.position.ReadValue();
            }
        }
    }
}
