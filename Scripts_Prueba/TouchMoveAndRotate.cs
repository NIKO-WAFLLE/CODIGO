using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveAndRotate : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float rotationSpeed = 1f;

    [Header("Límites de movimiento personalizados")]
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -5f;
    public float maxY = 5f;

    private Vector3 lastTouchWorldPos;
    private bool isTouchingObject = false;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    isTouchingObject = true;
                    lastTouchWorldPos = ScreenToWorld(touch.position);
                }
                else
                {
                    isTouchingObject = false;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isTouchingObject)
            {
                Vector3 currentWorldPos = ScreenToWorld(touch.position);
                Vector3 delta = currentWorldPos - lastTouchWorldPos;

                Vector3 newPos = transform.position + delta * moveSpeed;

                // Limitar dentro de los límites personalizados
                newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
                newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

                transform.position = newPos;
                lastTouchWorldPos = currentWorldPos;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouchingObject = false;
            }
        }

        if (Input.touchCount == 2)
        {
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            Vector2 t1Prev = t1.position - t1.deltaPosition;
            Vector2 t2Prev = t2.position - t2.deltaPosition;

            float prevAngle = Vector2.SignedAngle(t2Prev - t1Prev, Vector2.right);
            float currentAngle = Vector2.SignedAngle(t2.position - t1.position, Vector2.right);

            float angleDelta = currentAngle - prevAngle;

            transform.Rotate(Vector3.up, -angleDelta * rotationSpeed, Space.World);
        }
    }

    Vector3 ScreenToWorld(Vector2 screenPos)
{
    // Usa la distancia desde la cámara al objeto como referencia de profundidad
    float distanceToCamera = Vector3.Distance(Camera.main.transform.position, transform.position);
    return Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, distanceToCamera));
}

    void OnDrawGizmosSelected()
    {
        // Dibuja un área visible de los límites en la escena
        Gizmos.color = Color.red;
        Vector3 bottomLeft = new Vector3(minX, minY, transform.position.z);
        Vector3 topLeft = new Vector3(minX, maxY, transform.position.z);
        Vector3 topRight = new Vector3(maxX, maxY, transform.position.z);
        Vector3 bottomRight = new Vector3(maxX, minY, transform.position.z);

        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }
}
